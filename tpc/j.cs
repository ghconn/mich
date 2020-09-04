using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace tpc
{
    /// <summary>
    ///  序列化和反序列化
    /// </summary>
    public class j
    {
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            string json = JsonConvert.SerializeObject(o);
            return json;
        }

        /// <summary>
        /// 将对象序列化为JSON格式,指定日期格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <param name="datetimeformat">日期格式，如："yyyy-MM-dd HH:mm:ss"</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o, string datetimeformat)
        {
            IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = datetimeformat };
            string json = JsonConvert.SerializeObject(o, dtConverter);
            return json;
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            return serializer.Deserialize<T>(new JsonTextReader(sr));
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }

        public static T ParseModel<T>(string JsonString)
        {
            return JsonConvert.DeserializeObject<T>(JsonString);
        }

        /// <summary>
        /// beta
        /// </summary>
        /// <param name="json"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DeserializeJObject(string json, params string[] key)
        {
            var jObject = Newtonsoft.Json.Linq.JObject.Parse(json);
            if (key.Length == 0)
                return jObject.ToString();

            Newtonsoft.Json.Linq.JToken jToken = jObject[key[0]];
            GetJToken(ref jToken, 1, key);
            
            return jToken.ToString();
        }

        private static void GetJToken(ref Newtonsoft.Json.Linq.JToken jToken, int i, params string[] key)
        {
            if (i < key.Length)
            {
                jToken = jToken[key[i]];
                GetJToken(ref jToken, ++i, key);
            }
        }
    }
}
