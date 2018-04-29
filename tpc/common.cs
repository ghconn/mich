using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

namespace tpc
{
    public static class common
    {
        #region 获取中英文混排字符串的实际长度(字节数)
        /// <summary>
        /// 获取中英文混排字符串的实际长度(字节数)
        /// </summary>
        /// <param name="str">要获取长度的字符串</param>
        /// <returns>字符串的实际长度值（字节数）</returns>
        public static int CLength(this string str)
        {
            if (str.Equals(string.Empty))
                return 0;
            int strlen = 0;
            ASCIIEncoding strData = new ASCIIEncoding();
            //将字符串转换为ASCII编码的字节数字
            byte[] strBytes = strData.GetBytes(str);
            for (int i = 0; i < strBytes.Length; i++)
            {
                if (strBytes[i] == 63)  //中文都将编码为ASCII编码63,即"?"号
                    strlen++;
                strlen++;
            }
            return strlen;
        } 
        #endregion

        #region 获取当前应用程序所在的目录"/"
        /// <summary>
        /// 获取当前应用程序所在的目录
        /// </summary>
        public static String GetCurrentPath
        {
            get
            {
                String path = HttpContext.Current.Request.ApplicationPath;
                if (!path.EndsWith("/"))
                {
                    path += "/";
                }
                return path;
            }
        } 
        #endregion

        #region 获取当前请求文件所在的物理目录
        /// <summary>
        /// 获取当前请求文件所在的物理目录
        /// </summary>
        public static String GetCurrentPhysicalPath
        {
            get
            {
                String path = HttpContext.Current.Request.PhysicalApplicationPath;
                if (!path.EndsWith("\\"))
                {
                    path += "\\";
                }
                return path;
            }
        } 
        #endregion

        #region 获得当前请求的客户端IP
        /// <summary>
        /// 获得当前请求的客户端IP
        /// </summary>
        /// <returns>当前请求的客户端IP</returns>
        public static string GetIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]; //GetDnsRealHost();?原文这里没有注释，貌似是多写的
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.UserHostAddress;
            if (string.IsNullOrEmpty(result) || IsIP(result))
                return "127.0.0.1";
            return result;
        } 
        #endregion

        #region 检查是否为IP地址
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        #endregion

        #region 截取字符长度
        /// <summary>
        /// 截取字符长度
        /// </summary>
        /// <param name="inputString">字符</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string CutString(string inputString, int len)
        {
            if (string.IsNullOrEmpty(inputString))
                return "";
            inputString = DropHTML(inputString);
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }
            //如果截过则加上半个省略号 
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString += "…";
            return tempString;
        }
        #endregion

        #region 清除HTML标记
        public static string DropHTML(string Htmlstring)
        {
            if (string.IsNullOrEmpty(Htmlstring)) return "";
            //删除脚本  
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML  
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring.Replace("&emsp;", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
        #endregion

        #region 清除HTML标记且返回相应的长度
        public static string DropHTML(string Htmlstring, int strLen)
        {
            return CutString(DropHTML(Htmlstring), strLen);
        }
        #endregion

        #region 检测是否是安全的，可以直接拼接到sql语句中的，没有包含特殊命令的字符串
        public static bool IsSafeSqlString(string str)
        {
            //return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
            return !Regex.IsMatch(str, @"[-;,\/\(\)\[\]\}\{%@\*!\']");
        }
        #endregion

        #region 读取或写入cookie
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = UrlEncode(strValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = UrlEncode(strValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
                return UrlDecode(HttpContext.Current.Request.Cookies[strName].Value.ToString());
            return "";
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName, string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
                return UrlDecode(HttpContext.Current.Request.Cookies[strName][key].ToString());

            return "";
        }
        #endregion

        #region URL En-De处理
        /// <summary>
        /// URL字符编码
        /// </summary>
        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            str = str.Replace("'", "");
            return HttpContext.Current.Server.UrlEncode(str);
        }

        /// <summary>
        /// URL字符解码
        /// </summary>
        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return HttpContext.Current.Server.UrlDecode(str);
        }
        #endregion

        #region 字符串转换为Unicode十六进制表现形式
        /// <summary>
        /// "转换为Unicode十六进制表现形式"=>"\u8f6c\u6362\u4e3a\u0055\u006e\u0069\u0063\u006f\u0064\u0065\u5341\u516d\u8fdb\u5236\u8868\u73b0\u5f62\u5f0f"
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string strtohexunicode(string s)
        {
            return string.Concat(s.Select(c => "\\u" + ((int)c).ToString("x4")));
        } 
        #endregion

        #region 数据导出到excel csv文件
        /// <summary>
        /// 数据导出到excel csv文件，指定文件名，编码方式，列头，日期字段格式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">如：Order实体List</param>
        /// <param name="fileName">文件名</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="columnheader">列头（每一列的列头用","分隔合并在一起，并在最后添加换行符"\n"）</param>
        /// <param name="dateformat">日期格式(如果有日期类型成员，没有时为"")</param>
        /// <param name="properties">data中的对象的要把值导出的属性名集合，所有属性名用","分隔合并在一起，并要和相应的列头顺序对应</param>
        /// <param name="saveastextprops">某些属性成员的值可能全是数字，但要保存为文本格式，将这些所有属性名用","分隔合并在一起</param>
        public static void DataToCsv<T>(IEnumerable<T> data, string fileName, Encoding encoding, string columnheader, string dateformat, string properties, string saveastextprops)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.Buffer = true;
            context.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + ".csv");
            context.Response.ContentEncoding = encoding;
            context.Response.ContentType = "application/vnd.ms-excel";

            var _content = new StringBuilder();
            _content.Append(columnheader);
            if (data.Count() > 0)
            {
                foreach (var _obj in data)
                {
                    foreach (var s in properties.Split(','))
                    {
                        var p = _obj.GetType().GetProperty(s);
                        var celltext = p.GetValue(_obj, null);
                        if (p.PropertyType == typeof(DateTime))
                            celltext = ((DateTime)p.GetValue(_obj, null)).ToString(dateformat);
                        if (saveastextprops.Split(',').Contains(s))
                            celltext = "\"\t" + celltext + "\"";
                        _content.Append(celltext + ",");//最后会多一个逗号，多一个单元格
                    }
                    _content.Append("\n");//最后会多一个换行
                }
            }
            context.Response.Write(_content);
        }
        #endregion

        #region 数据导出到excel csv文件
        /// <summary>
        /// 数据导出到excel csv文件，指定文件名，编码方式，列头，日期字段格式
        /// </summary>
        /// <param name="rows">如：dt.Select()</param>
        /// <param name="fileName">文件名</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="csvcolumnheader">列头（每一列的列头用","分隔合并在一起）</param>
        /// <param name="dateformat">日期格式(如果有日期类型成员，没有时为"")</param>
        /// <param name="captions">datarow中值要导出的列名集合，所有列名用","分隔合并在一起，并要和相应的列头顺序对应</param>
        /// <param name="saveastextcolumns">某些列的值可能全是数字，但要保存为文本格式，将这些所有列名用","分隔合并在一起</param>
        public static void DataToCsv(DataRow[] rows, string fileName, Encoding encoding, string csvcolumnheader, string dateformat, string captions, string saveastextcolumns)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.Buffer = true;
            context.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + ".csv");
            context.Response.ContentEncoding = encoding;
            context.Response.ContentType = "application/vnd.ms-excel";

            var _content = new StringBuilder();
            _content.Append(csvcolumnheader).Append("\n");
            if (rows.Length > 0)
            {
                foreach (var row in rows)
                {
                    foreach (var s in captions.Split(','))
                    {
                        var celltext = row[s].ToString(); ;
                        if (row.Table.Columns[s].DataType == typeof(DateTime))
                            celltext = (DateTime.Parse(celltext)).ToString(dateformat);
                        if (saveastextcolumns.Split(',').Contains(s))
                            celltext = "\"\t" + celltext + "\"";
                        _content.Append(celltext + ",");//最后会多一个逗号，多一个单元格
                    }
                    _content.Append("\n");//最后会多一个换行
                }
            }
            context.Response.Write(_content);
        }
        #endregion

        #region 将DataTable转换成Json
        /// <summary>
        /// 将DataTable转换成Json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(System.Data.DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                string[] types = { "Int32", "Decimal", "Double", "Int64" };//Int32,String,DateTime,Decimal,Double,Int64,Boolean

                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        var type = dt.Columns[j].DataType.Name;
                        if (dt.Rows[i][j] is DBNull)
                        {
                            sb.AppendFormat("\"{0}\":null", dt.Columns[j].ColumnName);
                        }
                        else if (Array.IndexOf(types, type) != -1)
                        {
                            sb.AppendFormat("\"{0}\":{1}", dt.Columns[j].ColumnName, dt.Rows[i][j]);
                        }
                        else if (type == "Boolean")
                        {
                            sb.AppendFormat("\"{0}\":{1}", dt.Columns[j].ColumnName, dt.Rows[i][j].ToString().ToLower());
                        }
                        else
                        {
                            sb.AppendFormat("\"{0}\":\"{1}\"", dt.Columns[j].ColumnName, dt.Rows[i][j]);
                        }                        
                        if (j < dt.Columns.Count - 1)
                            sb.Append(",");
                    }
                    sb.Append("}");
                    if (i < dt.Rows.Count - 1)
                        sb.Append(",");
                }
                sb.Append("]");
                return sb.ToString();
            }
            else
                return "[]";
        }
        #endregion
        
    }

}
