using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace tpc
{
    public class x
    {
        public static string XMLSerialize<T>(T entity)
        {
            StringBuilder buffer = new StringBuilder();

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextWriter writer = new StringWriter(buffer))
            {
                serializer.Serialize(writer, entity);
            }

            return buffer.ToString();
        }

        public static T DeXMLSerialize<T>(string xmlString)
        {
            T cloneObject = default(T);

            StringBuilder buffer = new StringBuilder();
            buffer.Append(xmlString);

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(buffer.ToString()))
            {
                Object obj = serializer.Deserialize(reader);
                cloneObject = (T)obj;
            }

            return cloneObject;
        }


        public static void XerObjToStream<T>(Stream stream, T t, Encoding encoding = null)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            XmlTextWriter xmlWriter = new XmlTextWriter(stream, (encoding ?? Encoding.UTF8));
            xmlSerializer.Serialize(xmlWriter, t);
        }
    }
}
