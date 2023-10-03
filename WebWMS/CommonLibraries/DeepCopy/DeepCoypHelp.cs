using Newtonsoft.Json;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static CommonLibraries.DeepCopy.DeepCoypHelp;

namespace CommonLibraries.DeepCopy
{
    public class DeepCoypHelp
    {
        /// <summary>
        /// Json深度拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopyJson<T>(T obj)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }


        /// <summary>
        /// 反射深度拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopyReflection<T>(T obj)
        {
            var type = obj.GetType();
            object o = Activator.CreateInstance(type);
            System.Reflection.PropertyInfo[] PI = type.GetProperties();
            for (int i = 0; i < PI.Count(); i++)
            {
                System.Reflection.PropertyInfo P = PI[i];
                P.SetValue(o, P.GetValue(obj));
            }
            return (T)o;
        }


        /// <summary>
        /// 表达式树深度复制
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        public static class TransExp<TIn, TOut>
        {
            private static readonly Func<TIn, TOut> cache = GetFunc();
            private static Func<TIn, TOut> GetFunc()
            {
                ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
                List<MemberBinding> memberBindingList = new List<MemberBinding>();

                foreach (var item in typeof(TOut).GetProperties())
                {
                    if (!item.CanWrite) continue;
                    MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }

                MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
                Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[] { parameterExpression });

                return lambda.Compile();
            }

            public static TOut Trans(TIn tIn)
            {
                return cache(tIn);
            }
        }

        /// <summary>
        /// 集合深复制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<T> DeepCopyList<T>(List<T> t)
        {
            List<T> list=new List<T>();
            if (t != null && t.Count > 0)
            {
                foreach(var l in t)
                {
                    list.Add(TransExp<T, T>.Trans(l));
                }
            }
            return list;
        }
    }
}
