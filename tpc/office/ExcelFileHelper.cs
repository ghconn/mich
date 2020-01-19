using mdl.Attributes;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

#pragma warning disable 1591

namespace tpc.office
{
    /// <summary>
    /// 导入/导出Excel帮助类
    /// </summary>
    public class ExcelFileHelper
    {
        /// <summary>
        /// 获取excel路径
        /// </summary>
        /// <param name="type"></param>
        /// <param name="customfilename"></param>
        /// <param name="urlAddress"></param>
        /// <returns></returns>
        public string GetCustomExcelFilePath(string type, string customfilename, out string urlAddress)
        {
            //文件夹
            string sRootFolder = Directory.GetCurrentDirectory();//_hostingEnvironment.WebRootPath;
            var dateNow = DateTime.Now;
            var datePath = $@"{dateNow.Year}{dateNow.Month}";
            var directoryPath = $"{sRootFolder}{Path.DirectorySeparatorChar}{type}{Path.DirectorySeparatorChar}{datePath}";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            //文件名
            string sFileName = $"{customfilename}.xlsx";
            urlAddress = $"/{type}/{datePath}/{sFileName}";
            return Path.Combine(directoryPath, sFileName);
        }

        #region spire.xls导出
        /// <returns>excel url</returns>
        public string CreateWorkbook<T>(List<List<T>> list, List<string> sheetNameList, string fileName) where T : new()
        {
            var filePath = GetCustomExcelFilePath("Export", fileName, out string urlAddress);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (var ms = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                Workbook workbook = new Workbook();

                var i = 0;
                foreach(var l in list)
                {
                    CreateSheet(l, workbook, i, sheetNameList[i]);
                    i++;
                }

                workbook.SaveToStream(ms, FileFormat.Version2007);
            }

            return urlAddress;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <param name="list3"></param>
        /// <param name="fileName">excel文件名</param>
        /// <returns>excel url</returns>
        public string CreateWorkbook<T, Q, R>(List<T> list1, List<Q> list2, List<R> list3, string fileName)
            where T : new () where Q : new() where R : new()
        {
            var filePath = GetCustomExcelFilePath("Export", fileName, out string urlAddress);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (var ms = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                Workbook workbook = new Workbook();

                if (list1 != null)
                {
                    var titleAttribute = AttributesHelper.GetClassAttributeDescrition<T, TitleAttribute>();
                    var sheetName = titleAttribute == null ? "sheet1" : string.IsNullOrEmpty(titleAttribute.Title) ? "sheet1" : titleAttribute.Title;
                    CreateSheet(list1, workbook, 0, sheetName);
                }
                if(list2 != null)
                {
                    var titleAttribute = AttributesHelper.GetClassAttributeDescrition<Q, TitleAttribute>();
                    var sheetName = titleAttribute == null ? "sheet2" : string.IsNullOrEmpty(titleAttribute.Title) ? "sheet2" : titleAttribute.Title;
                    CreateSheet(list2, workbook, 1, sheetName);
                }
                if (list3 != null)
                {
                    var titleAttribute = AttributesHelper.GetClassAttributeDescrition<R, TitleAttribute>();
                    var sheetName = titleAttribute == null ? "sheet3" : string.IsNullOrEmpty(titleAttribute.Title) ? "sheet3" : titleAttribute.Title;
                    CreateSheet(list3, workbook, 2, sheetName);
                }
                workbook.SaveToStream(ms, FileFormat.Version2007);
            }

            return urlAddress;
        }

        private void CreateSheet<T>(List<T> list, Workbook workbook, int index, string sheetName) where T : new()
        {
            Worksheet sheet = workbook.Worksheets[index];
            sheet.Name = sheetName;

            var propertyInfos = AttributesHelper.GetProperties<T>();

            #region 表数据
            int j = 0, k = 0;
            foreach (var property in propertyInfos)
            {
                var attr = AttributesHelper.GetFieldAttributeDescrition<T, TitleAttribute>(property);
                var title = attr?.Title;
                if (title == null)
                {
                    continue;
                }

                #region 列头
                sheet.Range[1, k + 1].Text = title;
                #endregion

                int i = 0;
                foreach (var l in list)
                {
                    var dyg = sheet.Range[i + 2, j + 1];
                    var type = property.PropertyType;
                    var value = property.GetValue(l);
                    if (type == typeof(int) || type == typeof(short) || type == typeof(long) || type == typeof(uint) || type == typeof(byte)
                        || type == typeof(sbyte) || type == typeof(ulong))
                    {
                        dyg.NumberValue = double.Parse(property.GetValue(l).ToString());
                        dyg.NumberFormat = "#,##0";
                    }
                    else if (type == typeof(float) || type == typeof(double) || type == typeof(decimal))
                    {
                        dyg.NumberValue = double.Parse(property.GetValue(l).ToString());
                        dyg.NumberFormat = "#,##0.00";
                    }
                    else if (value == null)
                    {
                        dyg.Text = "";
                    }
                    else
                    {
                        dyg.Text = value.ToString();
                    }
                    //dyg.ColumnWidth = 22;
                    //dyg.Style.Borders[BordersLineType.EdgeLeft].LineStyle = LineStyleType.Thin;//边框
                    //dyg.Style.Borders[BordersLineType.EdgeRight].LineStyle = LineStyleType.Thin;
                    //dyg.Style.Borders[BordersLineType.EdgeTop].LineStyle = LineStyleType.Thin;
                    //dyg.Style.Borders[BordersLineType.EdgeBottom].LineStyle = LineStyleType.Thin;

                    i++;
                }
                j++;
                k++;
            }
            #endregion
        }

        /// <summary>
        /// 非通用导出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="mergeIndex">例：[[1, 1, 2, 1], [10, 1, 12, 1]]，第1行第1列到第2行第1列合并，第10行第1列到第12行第1列合并</param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string CreateWorkbook<T>(List<T> list, List<int[]> mergeIndex, string fileName) where T : new()
        {
            var filePath = GetCustomExcelFilePath("Export", fileName, out string urlAddress);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (var ms = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                Workbook workbook = new Workbook();

                if (list != null)
                {
                    var titleAttribute = AttributesHelper.GetClassAttributeDescrition<T, TitleAttribute>();
                    var sheetName = titleAttribute == null ? "sheet1" : string.IsNullOrEmpty(titleAttribute.Title) ? "sheet1" : titleAttribute.Title;
                    CreateSheet(list, workbook, 0, mergeIndex, sheetName);
                }
                workbook.SaveToStream(ms, FileFormat.Version2007);
            }

            return urlAddress;
        }

        private void CreateSheet<T>(List<T> list, Workbook workbook, int index, List<int[]> mergeIndex, string sheetName) where T : new()
        {
            Worksheet sheet = workbook.Worksheets[index];
            sheet.Name = sheetName;

            foreach(int[] indexs in mergeIndex)
            {
                sheet.Range[indexs[0], indexs[1], indexs[2], indexs[3]].Merge();
            }

            var propertyInfos = AttributesHelper.GetProperties<T>();

            #region 表数据
            int j = 0, k = 0;
            foreach (var property in propertyInfos)
            {
                var attr = AttributesHelper.GetFieldAttributeDescrition<T, TitleAttribute>(property);
                var title = attr?.Title;
                if (title == null)
                {
                    continue;
                }

                #region 列头
                sheet.Range[1, k + 1].Text = title;
                #endregion

                int i = 0;
                foreach (var l in list)
                {
                    var dyg = sheet.Range[i + 2, j + 1];
                    var type = property.PropertyType;
                    var value = property.GetValue(l);
                    if (type == typeof(int) || type == typeof(short) || type == typeof(long) || type == typeof(uint) || type == typeof(byte)
                        || type == typeof(sbyte) || type == typeof(ulong))
                    {
                        dyg.NumberValue = double.Parse(property.GetValue(l).ToString());
                        dyg.NumberFormat = "#,##0";
                    }
                    else if (type == typeof(float) || type == typeof(double) || type == typeof(decimal))
                    {
                        dyg.NumberValue = double.Parse(property.GetValue(l).ToString());
                        dyg.NumberFormat = "#,##0.00";
                    }
                    else if (value == null)
                    {
                        dyg.Text = "";
                    }
                    else
                    {
                        dyg.Text = value.ToString();
                    }
                    //dyg.ColumnWidth = 22;
                    //dyg.Style.Borders[BordersLineType.EdgeLeft].LineStyle = LineStyleType.Thin;//边框
                    //dyg.Style.Borders[BordersLineType.EdgeRight].LineStyle = LineStyleType.Thin;
                    //dyg.Style.Borders[BordersLineType.EdgeTop].LineStyle = LineStyleType.Thin;
                    //dyg.Style.Borders[BordersLineType.EdgeBottom].LineStyle = LineStyleType.Thin;

                    i++;
                }
                j++;
                k++;
            }
            #endregion
        }
        #endregion
    }
}
