using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace tpc
{
    public class rror
    {
        #region
        public static string Setconnstr1(string s, string u, string p)
        {
            dynamic Json = new
            {
                MessageID = 222,
                Url = "",
                Img = ""
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(Json);
        }
        #endregion

        #region 设置配置文件中的节点信息
        /// <summary>
        /// 调试不能用，执行可用，因为路径不一样
        /// </summary>
        /// <param name="connstr"></param>
        public static void EditAppSetting(string key, string value)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                //打开app.config
                xDoc.Load("winch.exe.config");
                //string key;
                XmlNode app;
                app = xDoc.SelectSingleNode("/configuration/appSettings/add[@key='" + key + "']");
                app.Attributes["value"].Value = value;
                //关闭
                xDoc.Save("winch.exe.config");
            }
            catch { throw; }
        }
        #endregion

        #region 设置配置文件中的节点信息
        /// <summary>
        /// 不能用
        /// </summary>
        /// <param name="connstr"></param>
        public static void SaveConfig(string connstr)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            string strFileName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            doc.Load(strFileName);
            //找出名称为“add”的所有元素
            XmlNode node = doc.SelectSingleNode("configuration").SelectSingleNode("connectionStrings").SelectSingleNode("add");
            node.Attributes["connectionString"].Value = connstr;
            //保存上面的修改
            doc.Save(strFileName);
        }
        #endregion
    }
}
