using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using mdl;
using tpc;

namespace wes.ashx
{
    public class api : IHttpHandler
    {
        #region 所有接口入口及共用变量
        HttpRequest Request;
        HttpResponse Response;
        int pageindex, pagesize;
        public void ProcessRequest(HttpContext context)
        {
            ////指定POST请求方式
            //if (context.Request.HttpMethod == "GET")
            //{
            //    context.Response.Write(error("不支持的请求方式"));
            //    return;
            //}

            #region 根据action得到请求方法
            string action = context.Request["action"].ToLower();
            System.Reflection.MethodInfo method = this.GetType().GetMethod(action);
            if (method == null)
            {
                context.Response.Write(error("接口名错误"));
                return;
            }
            #endregion

            #region 赋值所有接口都用到的几个变量
            Request = context.Request;
            Response = context.Response;
            Response.ContentType = "text/plain";
            pageindex = int.TryParse(Request["pageindex"], out pageindex) ? pageindex : 1;
            pagesize = int.TryParse(Request["pagesize"], out pagesize) ? pagesize : 10;
            #endregion

            #region 调用方法处理并响应请求
            try
            {
                method.Invoke(this, null);
            }
            catch
            {
                Response.Write(fail);
            }
            //catch (Exception e)
            //{
            //    Response.Write(e.Message + "\n\n" + e.StackTrace);
            //    var ine = e.InnerException;
            //    while (ine != null)
            //    {
            //        Response.Write("\n\n\n\n");
            //        Response.Write(ine.Message + "\n\n" + ine.StackTrace);
            //        ine = ine.InnerException;
            //    }
            //}
            #endregion
        }
        #endregion

        public void abc()
        {
            //Response.StatusCode = 404;
            Response.Write(Request.Cookies["NSC_AAAC"]?.Value);
            Response.Write(string.Join(";", Request.UserLanguages));
        }

        public void d()
        {
            Response.ContentType = "image/png";
            //var bts = bof.StreamToBytes(new FileStream(@"E:\Source\MICH\bp\pic\prototype.png", FileMode.Open));
            //Response.OutputStream.Write(bts, 0, bts.Length);

            Response.WriteFile(@"E:\Source\MICH\bp\pic\prototype.png");
        }

        public void xml()
        {
            var rq = Request.InputStream;
            int length = 256;
            byte[] bts = new byte[rq.Length];

            //MemoryStream ms = new MemoryStream();
            //int i = 0;
            //while ((i = rq.Read(bts, 0, length)) > 0)
            //{
            //    ms.Write(bts, 0, i);
            //}
            //Response.Write(Encoding.Default.GetString(ms.ToArray()));

            Response.SetCookie(new HttpCookie("fiddler", Request.Headers["custom"]));
            Response.Write("start");
            int i = 0;
            while ((i = rq.Read(bts, 0, length)) > 0)
            {
                Response.OutputStream.Write(bts, 0, i);
            }
            Response.Write("end");
        }

        #region IsReusable
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion

        #region 成功或失败返回JSON
        string ss
        {
            get { return "{\"statu\":1,\"message\":\"success\"}"; }
        }
        string sx(string message)
        {
            return "{\"statu\":1,\"message\":\"" + message + "\"}";
        }
        /// <summary>
        /// 返回statu,message和用户任意添加的键值对
        /// </summary>
        /// <param name="keyvalue"></param>
        /// <returns></returns>
        string su(params object[] keyvalue)
        {
            var re = "{\"statu\":1,\"message\":\"success\"";
            for (var i = 0; i < keyvalue.Length; i += 2)
            {
                re += string.Format(",\"{0}\":\"{1}\"", keyvalue[i], keyvalue[i + 1]);
            }
            return re + "}";
        }
        /// <summary>
        /// 返回单个实体序列化JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">实体对象</param>
        /// <param name="message"></param>
        /// <param name="key">json字符串中实体对象作为值,":"号前面的key</param>
        /// <returns></returns>
        string sx<T>(T model, string message = "success", string key = "data") where T : new()
        {
            return "{\"statu\":1,\"message\":\"" + message + "\",\"" + key + "\":" + j.SerializeObject(model, "yyyy-MM-dd HH:mm:ss") + "}";
        }

        /// <summary>
        /// 返回集合序列化JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tist"></param>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string sx<T>(IEnumerable<T> list, string message = "success", string key = "data") where T : new()
        {
            return "{\"statu\":1,\"message\":\"" + message + "\",\"" + key + "\":" + j.SerializeObject(list, "yyyy-MM-dd HH:mm:ss") + "}";
        }

        string sx(DataTable dt, string message = "success", string key = "data")
        {
            return "{\"statu\":1,\"message\":\"" + message + "\",\"" + key + "\":" + common.DataTableToJson(dt) + "}";
        }
        //加上总记录数
        string sx<T>(IEnumerable<T> list, int count, string message = "success", string key = "data") where T : new()
        {
            return "{\"statu\":1,\"message\":\"" + message + "\",\"count\":" + count + ",\"" + key + "\":" + j.SerializeObject(list, "yyyy-MM-dd HH:mm:ss") + "}";
        }
        //加上总记录数
        string sx(DataTable dt, int count, string message = "success", string key = "data")
        {
            return "{\"statu\":1,\"message\":\"" + message + "\",\"count\":" + count + ",\"" + key + "\":" + common.DataTableToJson(dt) + "}";
        }

        string fail
        {
            get { return "{\"statu\":0,\"message\":\"failure\"}"; }
        }

        string error(string message)
        {
            return "{\"statu\":0,\"message\":\"" + message + "\"}";
        }
        #endregion
    }
}