﻿using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Azure.Management.Redis.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraries.Redis
{
    public class RedisClientHelper
    {
        #region   基础参数

        //连接字符串
        private readonly string _ConnectStr;
        //连接的Redis对象
        private static IConnectionMultiplexer? _ConnMultiplexer;
        //锁
        private static readonly object _Locker = new object();
        private readonly RedisSetting options;

        //数据库
        private IDatabase _DB;

        #endregion

        #region   公有方法

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connStr">Redis的连接字符串</param>
        public RedisClientHelper(IOptionsSnapshot<RedisSetting> options)
        {
            this.options = options.Value;
            //获取字符串
            this._ConnectStr = string.Format("{0}:{1}", options.Value.Ip, options.Value.Port);
            //获取到连接的Redis对象
            GetConnectRedisMultiplexer();
            //设置选择数据
            SelectDB(options.Value.DataBase);
            //添加注测事件
            AddRegisterEvent();
            
        }

        /// <summary>
        /// 选择数据库
        /// </summary>
        /// <param name="db">数据库编号（默认是编号0的数据库）</param>
        public void SelectDB(int db = 0)
        {
            _DB = _ConnMultiplexer.GetDatabase(db);
        }

        /// <summary>
        /// 获取到连接状态
        /// </summary>
        public bool ConnetStatus
        {
            get
            {
                if (_ConnMultiplexer != null)
                {
                    return _ConnMultiplexer.IsConnected;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 给键添加前缀
        /// </summary>
        /// <param name="prefixName">前缀名称</param>
        /// <param name="keyName">键名称</param>
        /// <returns>返回添加前缀后的键</returns>
        public string AddPrefixOfKey(string prefixName, string keyName)
        {
            string str = $"{prefixName}:{keyName}";
            return str;
        }

        //序列化类型
        public enum SerializeType
        {
            Binary,   //二进制
            Json,     //Json
        }


        /// <summary>
        /// 模糊匹配查询键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<string> Getkeys(string key)
        {
            List<string> keyList = new List<string>();
            ConnectionMultiplexer conn = ConnectionMultiplexer.Connect(_ConnectStr);
            var db = conn.GetEndPoints();

            foreach (var ep in db)
            {
                var server = _ConnMultiplexer.GetServer(ep);
                var keys = server.Keys(0, $"*{key}*");
                foreach (var item in keys)
                {
                    keyList.Add((string)item);
                }
            }
            return keyList;
        }


        /// 模糊匹配删除键值
        /// </summary>
        /// <param name="pattern">pattern</param>
        public virtual void RemoveByPattern(string pattern)
        {
            ConnectionMultiplexer conn = ConnectionMultiplexer.Connect(_ConnectStr);
            var db = conn.GetEndPoints();
            foreach (var ep in db)
            {
                var server = conn.GetServer(ep);
                var keys = server.Keys(pattern: "*" + pattern + "*");
                foreach (var key in keys)
                {
                    _DB.KeyDelete(key);
                }
            }
        }
        #region 数据操作

        #region   String 操作

        #region   同步方式
        /// <summary>
        /// 保存单个键值对字符串（如果 key 已存在，则覆盖值）
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="redisValue">值</param>
        /// <param name="expiry">时间间隔</param>
        /// <returns>返回结果（true表示成功）</returns>
        public bool SetString(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            if (ConnetStatus)
            {
                return _DB.StringSet(redisKey, redisValue, expiry);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取键对应值字符串
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="expiry">时间间隔</param>
        /// <returns>返回结果（true表示成功）</returns>
        public string GetString(string redisKey, TimeSpan? expiry = null)
        {
            if (ConnetStatus)
            {
                return _DB.StringGet(redisKey);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 保存多个多个键值对字符串
        /// </summary>
        /// <param name="keyValuePairs">键值对容器</param>
        /// <returns>返回结果（true表示成功）</returns>
        public bool SetString(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            if (ConnetStatus)
            {
                var pairs = keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(x.Key, x.Value));
                return _DB.StringSet(pairs.ToArray());
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="redisValue">值（会被序列化）</param>
        /// <param name="expiry">时间间隔</param>
        /// <param name="serializeType">序列化类型（默认序列化为Json）</param>
        /// <returns>返回结果（true表示成功）</returns>
        public bool SetObjString<T>(string redisKey, T redisValue, TimeSpan? expiry = null,
            SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        var binaryValue = Serialize(redisValue);
                        return _DB.StringSet(redisKey, binaryValue, expiry);

                    case SerializeType.Json:
                        var jsonValue = SerializeJson(redisValue);
                        return _DB.StringSet(redisKey, jsonValue, expiry);

                    default:
                        return false;

                }
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// 获取一个对象（会进行反序列化）
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="expiry"></param>
        /// <param name="serializeType">序列化类型（默认序列化为Json）</param> 
        /// <returns>返回结果（true表示成功）</returns>
        public T GetObjString<T>(string redisKey, TimeSpan? expiry = null,
            SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        return Deserialize<T>(_DB.StringGet(redisKey));

                    case SerializeType.Json:
                        return DeserializeJson<T>(_DB.StringGet(redisKey));

                    default:

                        return default(T);
                }
            }
            else
            {

                return default(T);
            }

        }



        #endregion

        #region 异步方式

        /// <summary>
        /// 保存一个键值对字符串（异步方式）
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="redisValue">值</param>
        /// <param name="expiry">时间间隔</param>
        /// <returns>返回结果（true表示成功）</returns>
        public async Task<bool> SetStringOfAsync(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            if (ConnetStatus)
            {
                return await _DB.StringSetAsync(redisKey, redisValue, expiry);
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="redisValue">值</param>
        /// <param name="expiry">时间间隔</param>
        /// <returns>返回结果（true表示成功）</returns>
        public async Task<string> GetStringOfAsync(string redisKey, TimeSpan? expiry = null)
        {
            if (ConnetStatus)
            {
                return await _DB.StringGetAsync(redisKey);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 保存一组字符串值
        /// </summary>
        /// <param name="keyValuePairs">键值对容器</param>
        /// <returns>返回结果（true表示成功）</returns>
        public async Task<bool> SetStringOfAsync(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            if (ConnetStatus)
            {
                var pairs = keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(x.Key, x.Value));
                return await _DB.StringSetAsync(pairs.ToArray());
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="redisValue">值</param>
        /// <param name="expiry">间隔时间</param>
        /// <param name="serializeType">序列化类型（默认序列化为Json）</param> 
        /// <returns>返回结果（true表示成功）</returns>
        public async Task<bool> SetObjStringOfAsync<T>(string redisKey, T redisValue, TimeSpan? expiry = null,
            SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        var binaryValue = Serialize(redisValue);
                        return await _DB.StringSetAsync(redisKey, binaryValue, expiry);

                    case SerializeType.Json:
                        var jsonValue = SerializeJson(redisValue);
                        return await _DB.StringSetAsync(redisKey, jsonValue, expiry);

                    default:
                        break;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取一个对象（会进行反序列化）
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="expiry">间隔时间</param>
        /// <param name="serializeType">序列化类型（默认序列化为Json）</param> 
        /// <returns>返回结果（true表示成功）</returns>
        public async Task<T> GetObjStringOfAsync<T>(string redisKey, TimeSpan? expiry = null,
            SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        return Deserialize<T>(await _DB.StringGetAsync(redisKey));

                    case SerializeType.Json:
                        return DeserializeJson<T>(await _DB.StringGetAsync(redisKey));

                    default:
                        break;
                }
                return default(T);
            }
            else
            {
                return default(T);
            }

        }

        #endregion

        #endregion

        #region  Hash 操作

        #region   同步方式
        /// <summary>
        /// 判断该字段是否存在hash中
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="hashField">字段</param>
        /// <returns>返回结果（true：表示成功）</returns>
        public bool ExistsHash(string redisKey, string hashField)
        {
            if (ConnetStatus)
            {
                return _DB.HashExists(redisKey, hashField);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="hashField">字段</param>
        /// <returns>返回结果（true：表示成功）</returns>
        public bool DeleteHash(string redisKey, string hashField)
        {
            if (ConnetStatus)
            {
                return _DB.HashDelete(redisKey, hashField);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="hashFields">字段</param>
        /// <returns>返回结果（-1：表示失败）</returns>
        public long DeleteHash(string redisKey, IEnumerable<string> hashFields)
        {
            if (ConnetStatus)
            {
                var fields = hashFields.Select(x => (RedisValue)x);

                return _DB.HashDelete(redisKey, fields.ToArray());
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 在 hash 设定值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="hashField">字段</param>
        /// <param name="value">值</param>
        /// <returns>返回结果（true：表示成功）</returns>
        public bool SetHash(string redisKey, string hashField, string value)
        {
            if (ConnetStatus)
            {
                return _DB.HashSet(redisKey, hashField, value);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 在 hash 中设定值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="hashFields">字段容器</param>
        public void SetHash(string redisKey, IEnumerable<KeyValuePair<string, string>> hashFields)
        {
            if (ConnetStatus)
            {
                var entries = hashFields.Select(x => new HashEntry(x.Key, x.Value));

                _DB.HashSet(redisKey, entries.ToArray());
            }

        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="hashField">字段</param>
        /// <returns>返回结果</returns>
        public string GetHash(string redisKey, string hashField)
        {
            if (ConnetStatus)
            {
                return _DB.HashGet(redisKey, hashField);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 在hash获取值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="hashFields">字段</param>
        /// <returns>返回结果</returns>
        public IEnumerable<string> GetHash(string redisKey, IEnumerable<string> hashFields)
        {
            if (ConnetStatus)
            {
                var fields = hashFields.Select(x => (RedisValue)x);

                return ConvertStrings(_DB.HashGet(redisKey, fields.ToArray()));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 从 hash 返回所有的字段值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <returns>返回结果</returns>
        public IEnumerable<string> HashKeys(string redisKey)
        {
            if (ConnetStatus)
            {
                return ConvertStrings(_DB.HashKeys(redisKey));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据键获取hash中的所有值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <returns>返回hash结果</returns>
        public IEnumerable<string> HashValues(string redisKey)
        {
            if (ConnetStatus)
            {
                return ConvertStrings(_DB.HashValues(redisKey));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 在 hash 设定值（序列化）
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="hashField">字段</param>
        /// <param name="redisValue">值</param>
        /// <param name="serializeType">序列化的类型（默认是Json）</param>
        /// <returns>返回结果（true:表示成功）</returns>
        public bool SetObjHash<T>(string redisKey, string hashField, T redisValue,
            SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        var binaryValue = Serialize(redisValue);
                        return _DB.HashSet(redisKey, hashField, binaryValue);

                    case SerializeType.Json:
                        var jsonValue = SerializeJson(redisValue);
                        return _DB.HashSet(redisKey, hashField, jsonValue);

                    default:
                        return false;

                }

            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 在 hash 中获取值（反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="serializeType">序列化的类型（默认是Json）</param>
        /// <returns></returns>
        public T GetObjHash<T>(string redisKey, string hashField,
            SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        return Deserialize<T>(_DB.HashGet(redisKey, hashField));

                    case SerializeType.Json:
                        return DeserializeJson<T>(_DB.HashGet(redisKey, hashField));

                    default:
                        return default(T);
                }

            }
            else
            {
                return default(T);
            }
        }

        #endregion

        #region   异步方式

        /// <summary>
        /// 判断该字段是否存在hash中（异步方式）
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="hashField">字段</param>
        /// <returns>返回结果（true：表示存在）</returns>
        public async Task<bool> ExistsHashOfAsync(string redisKey, string hashField)
        {
            if (ConnetStatus)
            {
                return await _DB.HashExistsAsync(redisKey, hashField);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 从hash中移除指定字段
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="hashField">字段</param>
        /// <returns>返回结果（true：表示成功）</returns>
        public async Task<bool> DeleteHashOfAsync(string redisKey, string hashField)
        {
            if (ConnetStatus)
            {
                return await _DB.HashDeleteAsync(redisKey, hashField);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 从hash中移除指定字段
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="hashFields">字段</param>
        /// <returns>返回删除结果（-1 表示失败）</returns>
        public async Task<long> DeleteHashOfAsync(string redisKey, IEnumerable<string> hashFields)
        {
            if (ConnetStatus)
            {
                var fields = hashFields.Select(x => (RedisValue)x);

                return await _DB.HashDeleteAsync(redisKey, fields.ToArray());
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 在 hash 设定值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="hashField">字段</param>
        /// <param name="value">值</param>
        /// <returns>返回结果（true:表示成功）</returns>
        public async Task<bool> SetHashOfAsync(string redisKey, string hashField, string value)
        {
            if (ConnetStatus)
            {
                return await _DB.HashSetAsync(redisKey, hashField, value);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 在 hash 中设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashFields"></param>
        public async Task SetHashOfAsync(string redisKey, IEnumerable<KeyValuePair<string, string>> hashFields)
        {
            if (ConnetStatus)
            {
                var entries = hashFields.Select(x => new HashEntry(x.Key, x.Value));
                await _DB.HashSetAsync(redisKey, entries.ToArray());
            }
            else
            {
                return;
            }

        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="hashField">字段</param>
        /// <returns>返回结果</returns>
        public async Task<string> GetHashOfAsync(string redisKey, string hashField)
        {
            if (ConnetStatus)
            {
                return await _DB.HashGetAsync(redisKey, hashField);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="hashFields">字段</param>
        /// <param name="value">值</param>
        /// <returns>返回结果</returns>
        public async Task<IEnumerable<string>> GetHashOfAsync(string redisKey, IEnumerable<string> hashFields,
            string value)
        {
            if (ConnetStatus)
            {
                var fields = hashFields.Select(x => (RedisValue)x);

                return ConvertStrings(await _DB.HashGetAsync(redisKey, fields.ToArray()));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 从 hash 返回所有的字段值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <returns>返回对应的hash字段值</returns>
        public async Task<IEnumerable<string>> HashKeysOfAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return ConvertStrings(await _DB.HashKeysAsync(redisKey));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 返回 hash 中的所有值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <returns>返回hash中的所有值</returns>
        public async Task<IEnumerable<string>> HashValuesAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return ConvertStrings(await _DB.HashValuesAsync(redisKey));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 在 hash 设定值（序列化）
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="hashField">字段</param>
        /// <param name="value">值</param>
        /// <param name="serializeType">序列化的类型（默认是Json）</param>
        /// <returns>返回结果（true:表示成功）</returns>
        public async Task<bool> SetObjHashAsync<T>(string redisKey, string hashField, T value,
            SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        var binaryValue = Serialize(value);
                        return await _DB.HashSetAsync(redisKey, hashField, binaryValue);

                    case SerializeType.Json:
                        var jsonValue = SerializeJson(value);
                        return await _DB.HashSetAsync(redisKey, hashField, jsonValue);
                    default:
                        return false;
                }

            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 在 hash 中获取值（反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="serializeType">序列化的类型（默认是Json）</param>
        /// <returns></returns>
        public async Task<T> GetObjHashAsync<T>(string redisKey, string hashField,
            SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        return Deserialize<T>(await _DB.HashGetAsync(redisKey, hashField));
                    case SerializeType.Json:
                        return DeserializeJson<T>(await _DB.HashGetAsync(redisKey, hashField));
                    default:
                        return default(T);
                }

            }
            else
            {
                return default(T);
            }

        }

        #endregion

        #endregion

        #region   List 操作

        #region   同步方式

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <returns></returns>
        public string PopFirtKeyOfList(string redisKey)
        {
            if (ConnetStatus)
            {
                return _DB.ListLeftPop(redisKey);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <returns></returns>
        public string PopLastKeyOfList(string redisKey)
        {
            if (ConnetStatus)
            {
                return _DB.ListRightPop(redisKey);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 移除列表指定键上与该值相同的元素
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="redisValue">值</param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public long PopSameKeyOfList(string redisKey, string redisValue)
        {
            if (ConnetStatus)
            {
                return _DB.ListRemove(redisKey, redisValue);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="redisValue">值</param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public long PushKeyOfListLast(string redisKey, string redisValue)
        {
            if (ConnetStatus)
            {
                return _DB.ListRightPush(redisKey, redisValue);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="redisValue">值</param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public long PushKeyOfListFirst(string redisKey, string redisValue)
        {
            if (ConnetStatus)
            {
                return _DB.ListLeftPush(redisKey, redisValue);
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回 0
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public long GetKeyLengthOfList(string redisKey)
        {
            if (ConnetStatus)
            {
                return _DB.ListLength(redisKey);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="start">起始点</param>
        /// <param name="stop">停止点</param>
        /// <returns></returns>
        public IEnumerable<string> ListRange(string redisKey, long start = 0L, long stop = -1L)
        {
            if (ConnetStatus)
            {
                return ConvertStrings(_DB.ListRange(redisKey, start, stop));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素对象
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="serializeType">序列化类型（默认是Json）</param>
        /// <returns>返回反序列化后对象</returns>
        public T PopFirstKeyObjOfList<T>(string redisKey, SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        return Deserialize<T>(_DB.ListLeftPop(redisKey));

                    case SerializeType.Json:
                        return DeserializeJson<T>(_DB.ListLeftPop(redisKey));
                    default:
                        return default(T);
                }

            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素对象
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="serializeType">序列化类型（默认是Json）</param>
        /// <returns>返回反序列化后的对象</returns>
        public T PopLastKeyObjOfList<T>(string redisKey, SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        return Deserialize<T>(_DB.ListRightPop(redisKey));
                    case SerializeType.Json:
                        return DeserializeJson<T>(_DB.ListRightPop(redisKey));
                    default:
                        return default(T);
                }
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public long PushKeyOfListLast<T>(string redisKey, T redisValue)
        {
            if (ConnetStatus)
            {
                return _DB.ListRightPush(redisKey, Serialize(redisValue));
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public long PushKeyOfListHead<T>(string redisKey, T redisValue)
        {
            if (ConnetStatus)
            {
                return _DB.ListLeftPush(redisKey, Serialize(redisValue));
            }
            else
            {
                return -1;
            }
        }

        #endregion

        #region    异步方式

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<string> PopFirtKeyOfListAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return await _DB.ListLeftPopAsync(redisKey);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <returns></returns>
        public async Task<string> PopLastKeyOfListAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return await _DB.ListRightPopAsync(redisKey);
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 移除列表指定键上与该值相同的元素
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="redisValue">值</param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public async Task<long> PopSameKeyOfListAsync(string redisKey, string redisValue)
        {
            if (ConnetStatus)
            {
                return await _DB.ListRemoveAsync(redisKey, redisValue);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="redisValue">值</param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public async Task<long> PushKeyOfListLastAsync(string redisKey, string redisValue)
        {
            if (ConnetStatus)
            {
                return await _DB.ListRightPushAsync(redisKey, redisValue);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="redisValue">值</param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public async Task<long> PushKeyOfListHeadAsync(string redisKey, string redisValue)
        {
            if (ConnetStatus)
            {
                return await _DB.ListLeftPushAsync(redisKey, redisValue);
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回 0
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public async Task<long> GetKeyLengthOfListAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return await _DB.ListLengthAsync(redisKey);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> ListRangeAsync(string redisKey, long start = 0L, long stop = -1L)
        {
            if (ConnetStatus)
            {
                var query = await _DB.ListRangeAsync(redisKey, start, stop);
                return query.Select(x => x.ToString());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素对象
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="serializeType">序列化类型（默认是Json）</param>
        /// <returns>返回反序列化后的对象</returns>
        public async Task<T> PopFirstKeyObjOfListAsync<T>(string redisKey, SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        return Deserialize<T>(await _DB.ListLeftPopAsync(redisKey));
                    case SerializeType.Json:
                        return DeserializeJson<T>(await _DB.ListLeftPopAsync(redisKey));
                    default:
                        return default(T);
                }

            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素对象
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="serializeType">序列化类型（默认是Json）</param>
        /// <returns>返回反序列化后的对象</returns>
        public async Task<T> PopLastKeyObjOfListAsync<T>(string redisKey, SerializeType serializeType = SerializeType.Json)
        {
            if (ConnetStatus)
            {
                switch (serializeType)
                {
                    case SerializeType.Binary:
                        return Deserialize<T>(await _DB.ListRightPopAsync(redisKey));
                    case SerializeType.Json:
                        return DeserializeJson<T>(await _DB.ListRightPopAsync(redisKey));
                    default:
                        return default(T);
                }

            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="redisValue">值</param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public async Task<long> PushKeyOfListLastAsync<T>(string redisKey, T redisValue)
        {
            if (ConnetStatus)
            {
                return await _DB.ListRightPushAsync(redisKey, Serialize(redisValue));
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="redisValue">值</param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public async Task<long> PushKeyOfListHeadAsync<T>(string redisKey, T redisValue)
        {
            if (ConnetStatus)
            {
                return await _DB.ListLeftPushAsync(redisKey, Serialize(redisValue));
            }
            else
            {
                return -1;
            }
        }

        #endregion


        #endregion

        #region SortedSet 操作

        #region   同步方式
        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool SortedSetAdd(string redisKey, string member, double score)
        {
            if (ConnetStatus)
            {
                return _DB.SortedSetAdd(redisKey, member, score);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下从低到高
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public IEnumerable<string> SortedSetRangeByRank(string redisKey, long start = 0L, long stop = -1L,
            OrderType order = OrderType.Asc)
        {
            if (ConnetStatus)
            {
                return _DB.SortedSetRangeByRank(redisKey, start, stop, (Order)order).Select(x => x.ToString());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public long SortedSetLength(string redisKey)
        {
            if (ConnetStatus)
            {
                return _DB.SortedSetLength(redisKey);
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="memebr"></param>
        /// <returns>返回结果（true:表示成功）</returns>
        public bool SortedSetLength(string redisKey, string memebr)
        {
            if (ConnetStatus)
            {
                return _DB.SortedSetRemove(redisKey, memebr);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns>返回结果（true:表示成功）</returns>
        public bool SortedSetAdd<T>(string redisKey, T member, double score)
        {
            if (ConnetStatus)
            {
                var json = Serialize(member);
                return _DB.SortedSetAdd(redisKey, json, score);
            }
            else
            {
                return false;
            }


        }

        /// <summary>
        /// 增量的得分排序的集合中的成员存储键值键按增量
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public double SortedSetIncrement(string redisKey, string member, double value = 1)
        {
            if (ConnetStatus)
            {
                return _DB.SortedSetIncrement(redisKey, member, value);
            }
            else
            {
                return -1;
            }

        }

        #endregion

        #region  异步方式

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns>返回结果（true:表示成功）</returns>
        public async Task<bool> SortedSetAddAsync(string redisKey, string member, double score)
        {
            if (ConnetStatus)
            {
                return await _DB.SortedSetAddAsync(redisKey, member, score);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下从低到高。
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> SortedSetRangeByRankAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return ConvertStrings(await _DB.SortedSetRangeByRankAsync(redisKey));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public async Task<long> SortedSetLengthAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return await _DB.SortedSetLengthAsync(redisKey);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="memebr"></param>
        /// <returns>返回结果（true:表示成功）</returns>
        public async Task<bool> SortedSetRemoveAsync(string redisKey, string memebr)
        {
            if (ConnetStatus)
            {
                return await _DB.SortedSetRemoveAsync(redisKey, memebr);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetAddAsync<T>(string redisKey, T member, double score)
        {
            if (ConnetStatus)
            {
                var json = Serialize(member);
                return await _DB.SortedSetAddAsync(redisKey, json, score);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 增量的得分排序的集合中的成员存储键值键按增量
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public async Task<double> SortedSetIncrementAsync(string redisKey, string member, double value = 1)
        {
            if (ConnetStatus)
            {
                return await _DB.SortedSetIncrementAsync(redisKey, member, value);
            }
            else
            {
                return -1;
            }
        }

        #endregion

        /// <summary>
        /// Redis 排序类型
        /// </summary>
        public enum OrderType
        {
            Asc,
            Desc
        }

        #endregion

        #region   Key 操作

        #region   同步方式

        /// <summary>
        /// 删除指定Key
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <returns>返回结果（true：表示成功）</returns>
        public bool DeleteKey(string redisKey)
        {
            if (ConnetStatus)
            {
                return _DB.KeyDelete(redisKey);
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 删除指定Key列表
        /// </summary>
        /// <param name="redisKeys">键容器</param>
        /// <returns>移除指定键的数量(-1:表示失败)</returns>
        public long DeleteKey(IEnumerable<string> redisKeys)
        {
            if (ConnetStatus)
            {
                var keys = redisKeys.Select(x => (RedisKey)x);
                return _DB.KeyDelete(keys.ToArray());
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 检查Key是否存在
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <returns>返回结果（true：表示成功）</returns>
        public bool ExistsKey(string redisKey)
        {
            if (ConnetStatus)
            {
                return _DB.KeyExists(redisKey);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 重命名Key
        /// </summary>
        /// <param name="redisKey">原来的键</param>
        /// <param name="redisNewKey">新的键名</param>
        /// <returns>返回结果（true：表示成功）</returns>
        public bool RenameKey(string redisKey, string redisNewKey)
        {
            if (ConnetStatus)
            {
                return _DB.KeyRename(redisKey, redisNewKey);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 设置 Key 的时间
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="expiry">时间间隔</param>
        /// <returns>返回结果（true：表示成功）</returns>
        public bool SetKeyExpire(string redisKey, TimeSpan? expiry)
        {
            if (ConnetStatus)
            {
                return _DB.KeyExpire(redisKey, expiry);
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region   异步方式

        /// <summary>
        /// 移除指定 Key
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <returns>返回结果（true：表示成功）</returns>
        public async Task<bool> DeleteKeyAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return await _DB.KeyDeleteAsync(redisKey);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定 Key 列表
        /// </summary>
        /// <param name="redisKeys">键</param>
        /// <returns>返回结果（-1 表示失败）</returns>
        public async Task<long> DeleteKeyAsync(IEnumerable<string> redisKeys)
        {
            if (ConnetStatus)
            {
                var keys = redisKeys.Select(x => (RedisKey)x);
                return await _DB.KeyDeleteAsync(keys.ToArray());
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// 检查 Key 是否存在
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <returns>返回结果（true：表示成功）</returns>
        public async Task<bool> ExistsKeyAsync(string redisKey)
        {
            if (ConnetStatus)
            {
                return await _DB.KeyExistsAsync(redisKey);
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 重命名 Key
        /// </summary>
        /// <param name="redisKey">旧的键名称</param>
        /// <param name="redisNewKey">新的键名称</param>
        /// <returns>返回结果（true：表示成功）</returns>
        public async Task<bool> RenameKeyAsync(string redisKey, string redisNewKey)
        {
            if (ConnetStatus)
            {
                return await _DB.KeyRenameAsync(redisKey, redisNewKey);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 设置 Key 的时间
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="expiry">间隔时间</param>
        /// <returns>返回结果（true：表示成功）</returns>
        public async Task<bool> SetKeyExpireAsync(string redisKey, TimeSpan? expiry)
        {
            if (ConnetStatus)
            {
                return await _DB.KeyExpireAsync(redisKey, expiry);
            }
            else
            {
                return false;
            }

        }

        #endregion

        #endregion

        #region 发布订阅

        #region   同步方式
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="handle"></param>
        public void Subscribe(RedisChannel channel, Action<RedisChannel, RedisValue> handle)
        {
            var sub = _ConnMultiplexer.GetSubscriber();
            sub.Subscribe(channel, handle);
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public long Publish(RedisChannel channel, RedisValue message)
        {
            var sub = _ConnMultiplexer.GetSubscriber();
            return sub.Publish(channel, message);
        }

        /// <summary>
        /// 发布（使用序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public long Publish<T>(RedisChannel channel, T message)
        {
            var sub = _ConnMultiplexer.GetSubscriber();
            return sub.Publish(channel, Serialize(message));
        }

        #endregion

        #region 异步方式

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="handle"></param>
        public async Task SubscribeAsync(RedisChannel channel, Action<RedisChannel, RedisValue> handle)
        {
            var sub = _ConnMultiplexer.GetSubscriber();
            await sub.SubscribeAsync(channel, handle);
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<long> PublishAsync(RedisChannel channel, RedisValue message)
        {
            var sub = _ConnMultiplexer.GetSubscriber();
            return await sub.PublishAsync(channel, message);
        }

        /// <summary>
        /// 发布（使用序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<long> PublishAsync<T>(RedisChannel channel, T message)
        {
            var sub = _ConnMultiplexer.GetSubscriber();
            return await sub.PublishAsync(channel, Serialize(message));
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region   私有方法

        /// <summary>
        /// 获取到连接的Redis对象
        /// </summary>
        /// <returns></returns>
        private IConnectionMultiplexer GetConnectRedisMultiplexer()
        {
            if (_ConnMultiplexer == null || !_ConnMultiplexer.IsConnected)
            {
                lock (_Locker)
                {
                    if (_ConnMultiplexer == null || !_ConnMultiplexer.IsConnected)
                    {
                        _ConnMultiplexer = ConnectionMultiplexer.Connect(_ConnectStr);
                    }
                }
            }
            return _ConnMultiplexer;
        }

        #region 注册事件

        /// <summary>
        /// 添加注册事件
        /// </summary>
        private static void AddRegisterEvent()
        {
            _ConnMultiplexer.ConnectionRestored += ConnMultiplexer_ConnectionRestored;
            _ConnMultiplexer.ConnectionFailed += ConnMultiplexer_ConnectionFailed;
            _ConnMultiplexer.ErrorMessage += ConnMultiplexer_ErrorMessage;
            _ConnMultiplexer.ConfigurationChanged += ConnMultiplexer_ConfigurationChanged;
            _ConnMultiplexer.HashSlotMoved += ConnMultiplexer_HashSlotMoved;
            _ConnMultiplexer.InternalError += ConnMultiplexer_InternalError;
            _ConnMultiplexer.ConfigurationChangedBroadcast += ConnMultiplexer_ConfigurationChangedBroadcast;
        }

        /// <summary>
        /// 重新配置广播时（通常意味着主从同步更改）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConfigurationChangedBroadcast)}: {e.EndPoint}");
        }

        /// <summary>
        /// 发生内部错误时（主要用于调试）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_InternalError(object sender, InternalErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_InternalError)}: {e.Exception}");
        }

        /// <summary>
        /// 更改集群时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_HashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            Console.WriteLine(
                $"{nameof(ConnMultiplexer_HashSlotMoved)}: {nameof(e.OldEndPoint)}-{e.OldEndPoint} To {nameof(e.NewEndPoint)}-{e.NewEndPoint}, ");
        }

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConfigurationChanged(object sender, EndPointEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConfigurationChanged)}: {e.EndPoint}");
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ErrorMessage(object sender, RedisErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ErrorMessage)}: {e.Message}");
        }

        /// <summary>
        /// 物理连接失败时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConnectionFailed)}: {e.Exception}");
        }

        /// <summary>
        /// 建立物理连接时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConnectionRestored)}: {e.Exception}");
        }

        #endregion 注册事件

        #region 序列化-反序列化
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="list">数据列表</param>
        /// <returns>返回转为字符串后的数据列表</returns>
        private static IEnumerable<string> ConvertStrings<T>(IEnumerable<T> list) where T : struct
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            return list.Select(x => x.ToString());
        }

        /// <summary>
        /// 序列化（二进制）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                var data = memoryStream.ToArray();
                return data;
            }
        }


        /// <summary>
        /// 反序列化（二进制）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private static T Deserialize<T>(byte[] data)
        {
            if (data == null)
                return default(T);

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream(data))
            {
                var result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }


        /// <summary>
        /// 序列化对象为Json字符串
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">实体对象</param>
        /// <returns>返回序列化的字符串</returns>
        private static string SerializeJson<T>(T obj)
        {
            string tmp = null;
            if (obj != null)
            {
                tmp = JsonConvert.SerializeObject(obj);
            }
            return tmp;
        }

        /// <summary>
        /// 反序列化Json字符串为对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="strjson">字符串</param>
        /// <returns></returns>
        private static T DeserializeJson<T>(string strjson)
        {
            T tmp = default(T);
            if (!string.IsNullOrEmpty(strjson))
            {
                tmp = JsonConvert.DeserializeObject<T>(strjson);
            }

            return tmp;
        }

        #endregion

        #endregion
    }
}
