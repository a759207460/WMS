using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace CommonLibraries.Excel
{
    public class EPPlusHelper
    {

        /// <summary>
        /// 导出数据到Excel中
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ds"></param>
        public static async Task<bool> ExportExcel(string fileName, DataTable dt, CancellationToken cancellationToken)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }
            FileInfo file = new FileInfo(fileName);
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(fileName);
            }
            //在using语句里面我们可以创建多个worksheet，ExcelPackage后面可以传入路径参数
            //命名空间是using OfficeOpenXml
            using (ExcelPackage package = new ExcelPackage(file))
            {
                //foreach (DataTable dt in ds.Tables)
                //{
                //创建工作表worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("sheet1");
                //给单元格赋值有两种方式
                //worksheet.Cells[1, 1].Value = "单元格的值";直接指定行列数进行赋值
                //worksheet.Cells["A1"].Value = "单元格的值";直接指定单元格进行赋值
                worksheet.Cells.Style.Font.Name = "微软雅黑";
                worksheet.Cells.Style.Font.Size = 12;
                worksheet.Cells.Style.ShrinkToFit = true;//单元格自动适应大小
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (i == 0)
                        {
                            worksheet.Cells[i + 1, j + 1].Value = dt.Columns[j].ColumnName;
                            worksheet.Cells[i + 2, j + 1].Value = dt.Rows[i][j].ToString();
                        }
                        else
                        {
                            worksheet.Cells[i + 2, j + 1].Value = dt.Rows[i][j].ToString();
                        }

                    }
                }
                using (var cell = worksheet.Cells[1, 1, 1, dt.Columns.Count])
                {
                    //设置样式:首行居中加粗背景色
                    cell.Style.Font.Bold = false; //加粗
                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //水平居中
                    cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;     //垂直居中
                    cell.Style.Font.Size = 12;
                    cell.Style.Fill.PatternType = ExcelFillStyle.None;  //背景颜色
                    //cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(128, 128, 128));//设置单元格背景色
                }
                // }
                //保存
                await package.SaveAsAsync(file, cancellationToken);
            }
            return true;
        }


        /// <summary>
        /// 导出数据到Excel中
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="ds"></param>
        public static async Task<bool> ExportModleExcel(string modePath, string downLoadPath, DataTable dt, CancellationToken cancellationToken)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }
            FileInfo modelfile = new FileInfo(modePath);
            //在using语句里面我们可以创建多个worksheet，ExcelPackage后面可以传入路径参数
            //命名空间是using OfficeOpenXml
            using (ExcelPackage package = new ExcelPackage(modelfile))
            {
                if (package != null)
                {
                    ExcelWorksheet? ws = package.Workbook.Worksheets.Where(x => x.Name.ToLower() == "sheet1").FirstOrDefault();

                    if (ws != null)
                    {
                        //给单元格赋值有两种方式
                        //worksheet.Cells[1, 1].Value = "单元格的值";直接指定行列数进行赋值
                        //worksheet.Cells["A1"].Value = "单元格的值";直接指定单元格进行赋值
                        ws.Cells.Style.Font.Name = "微软雅黑";
                        ws.Cells.Style.Font.Size = 12;
                        ws.Cells.Style.ShrinkToFit = true;//单元格自动适应大小

                        for (int i = 1; i < dt.Rows.Count; i++)
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                ws.Cells[i + 1, j + 1].Value = dt.Rows[i][j].ToString();

                            }
                        }
                        using (var cell = ws.Cells[1, 1, 1, dt.Rows.Count])
                        {
                            //设置样式:首行居中加粗背景色
                            //cell.Style.Font.Bold = false; //加粗
                            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //水平居中
                            cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;     //垂直居中
                        }
                        //保存
                        await package.SaveAsAsync(downLoadPath, cancellationToken);
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }

            }
            return true;
        }


        /// <summary>
        /// 读取Excel数据
        /// </summary>
        /// <param name="fileName"></param>
        public static async Task<string> ReadExcel(Stream fileStream, CancellationToken cancellationToken)
        {
            return await Task.Run<string>(() =>
            {
                StringBuilder sb = new StringBuilder();
                string jsonStr = string.Empty;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                try
                {
                    using (ExcelPackage package = new ExcelPackage(fileStream))
                    {
                        var count = package.Workbook.Worksheets.Count;
                        for (int k = 1; k <= count; k++)  //worksheet是从1开始的
                        {
                            var workSheet = package.Workbook.Worksheets[k - 1];
                            sb.Append("[");

                            int row = workSheet.Dimension.Rows;
                            int col = workSheet.Dimension.Columns;
                            for (int i = 1; i < row; i++)
                            {
                                sb.Append("{");
                                for (int j = 1; j <= col; j++)
                                {
                                    if (j < col)
                                    {
                                        sb.Append("\"" + workSheet.Cells[1, j].Value.ToString() + "\":\"" + workSheet.Cells[i + 1, j].Value.ToString() + "\",");
                                    }
                                    else
                                    {
                                        sb.Append("\"" + workSheet.Cells[1, j].Value.ToString() + "\":\"" + workSheet.Cells[i + 1, j].Value.ToString() + "\"},");
                                    }
                                }
                            }
                            jsonStr = sb.ToString();
                            jsonStr = jsonStr.Substring(0, jsonStr.Length - 2) + "}]";
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                return jsonStr;
            }, cancellationToken);
        }


        /// <summary>
        /// list to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static DataTable ListToDt<T>(IList<T> collection)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new
            DataColumn(p.Name, p.PropertyType)).ToArray());
            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi?.GetValue(collection.ElementAt(i), null);
                        if(obj != null)
                        tempList.Add(obj);
                    }
                    if(tempList!=null&&tempList.Count > 0)
                    {
                        object[] array = tempList?.ToArray();
                        if (array != null)
                            dt.LoadDataRow(array, true);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// datatable to list
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertToModel<T>(DataTable dt) where T : new()
        {

            List<T> ts = new List<T>();// 定义集合
            Type type = typeof(T); // 获得此模型的类型
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    if (dt.Columns.Contains(tempName))
                    {
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }
}
