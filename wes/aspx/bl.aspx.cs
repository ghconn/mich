using mdl;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wes.aspx
{
    public partial class bl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string[] s = { "abc", "abc" };
            //Response.Write(NotHasRpt(s));
            //Response.Write(System.IO.Path.GetExtension(@"d:/1.txt"));
            //var s = tpc.HttpService.Get("http://www.baidu.com");
            //Response.Write(s);
            //point[] pts = new point[] { new point() { x = 2, y = 3 }, new point() { x = 2, y = 3 }, new point() { x = 3, y = 2 } };
            //Response.Write(NotHasRpt(pts, new MyCompareer<point>()));

            //Response.Write(Regex.Split("ssss(23)", "\\(\\d+\\)")[0]);
            //Response.Write(Regex.Match("td:nth-child(23)", "\\d+").Value);

            //var s = System.Reflection.Assembly.Load("bp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            
        }

        public bool NotHasRpt<T>(IEnumerable<T> enumrable, IEqualityComparer<T> equality = null)
        {
            return enumrable.Distinct(equality).Count() == enumrable.Count();
        }


        void 生成_修改mysql表和字段字符集及排序规则_脚本()
        {
            var x = "";//把x写到页面上，复制即可

            string connStr = "server=172.18.132.141;database=FrameBase;uid=root;pwd=123456;charset=utf8";
            System.Data.DataSet set = new System.Data.DataSet();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                string sql = "select table_name from information_schema.tables where table_schema='FrameBase' and table_type='base table'";
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(cmd);
                    mySqlDataAdapter.Fill(set);
                }
                conn.Close();
            }

            var dts = set.Tables[0];
            var dv = dts.DefaultView;
            for (var i = 0; i < dv.Count; i++)
            {
                var tbname = dv[i][0].ToString();

                x += $"ALTER TABLE {tbname} DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;<br />";
                
                System.Data.DataSet setcol = new System.Data.DataSet();
                using (MySqlConnection conn2 = new MySqlConnection(connStr))
                {
                    string sql2 = $@"select column_name,data_type,character_maximum_length from information_schema.columns 
where table_schema='ChaComSystem' and table_name='{tbname}' 
and data_type in ('varchar','text', 'longtext', 'char')";
                    conn2.Open();
                    using (MySqlCommand cmd2 = new MySqlCommand(sql2, conn2))
                    {
                        MySqlDataAdapter mySqlDataAdapter2 = new MySqlDataAdapter(cmd2);
                        mySqlDataAdapter2.Fill(setcol);
                    }
                    conn2.Close();
                }


                var dtcols = setcol.Tables[0];
                var dv2 = dtcols.DefaultView;
                for (var j = 0; j < dv2.Count; j++)
                {
                    var row = dv2[j];
                    var colname = row[0].ToString();

                    var datatype = row[1].ToString();
                    if (datatype == "varchar" || datatype == "char")
                    {
                        datatype += $"({row[2]})";
                    }
                    x += $"ALTER TABLE {tbname} CHANGE {colname} {colname} {datatype} CHARACTER SET utf8 COLLATE utf8_general_ci;<br />";
                }

                x += "<br />";
            }

            Response.Write(x);
        }
    }
}