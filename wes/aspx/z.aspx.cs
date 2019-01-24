using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using tpc;

#pragma warning disable 0618

namespace wes.aspx
{
    public partial class z : System.Web.UI.Page
    {
        protected string od;
        protected string endata;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(Decrypt("C43F2E8CC6C592ACDD2A846D8F985DB0"));
            
        }

        private void lllllllll()
        {
            var s = "".Split(',')[2];
        }

        public static string Decrypt(string text)
        {
            return Decrypt(text, "yzy");
        }

        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Decrypt(string text, string sKey)
        {
            try
            {
                var des = new DESCryptoServiceProvider();
                int len;
                len = text.Length / 2;
                byte[] inputByteArray = new byte[len];
                int x, i;
                for (x = 0; x < len; x++)
                {
                    i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
                    inputByteArray[x] = (byte)i;
                }
                des.Key =
                    Encoding.ASCII.GetBytes(
                        System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5")
                            .Substring(0, 8));
                des.IV =
                    Encoding.ASCII.GetBytes(
                        System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5")
                            .Substring(0, 8));
                var ms = new System.IO.MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.Default.GetString(ms.ToArray());
            }
            catch
            {
                return "";
            }
        }
    }
}