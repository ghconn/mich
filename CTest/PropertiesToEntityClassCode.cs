using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTest
{
    public static class PropertiesToEntityClassCode
    {
        /// <summary>
        /// 简单生成，只生成类名和字段，不引用、不声明命名空间，不添加字段特性，不判断字段类型，生成文件到桌面类名.cs
        /// </summary>
        public static void Generat(this object entity, string classname)
        {
            StringBuilder sb = new StringBuilder();
            //需要仔细校验entity是否传入了正确的值
            //需要仔细校验entity是否传入了正确的值
            //需要仔细校验entity是否传入了正确的值
            if (entity is ICollection)
            {
                sb.Append($"public class {classname} \r\n").Append("{\r\n");
                var ic = (ICollection)entity;
                foreach (var s in ic)
                {
                    sb.Append($"\tpublic string {s}").Append(" { get; set; }\r\n");
                }
                sb.Append("}");

                tpc.f.TextAppendToFile($"{classname}.cs", sb.ToString());
            }
            else if (entity.GetType().IsArray || (entity.GetType().IsGenericType && entity.GetType().GetInterface("IEnumerable") != null))
            {
                IEnumerable ie = (IEnumerable)entity;
                var obj_it = ie.OfType<object>();
                var obj = obj_it.FirstOrDefault();
                obj.Generat(classname);
            }
            else
            {
                sb.Append($"public class {classname} \r\n").Append("{\r\n");
                var propertyinfo = entity.GetType().GetProperties();
                foreach (var p in propertyinfo)
                {
                    sb.Append($"\tpublic string {p.Name}").Append(" { get; set; }\r\n");
                }
                sb.Append("}");

                tpc.f.TextAppendToFile($"{classname}.cs", sb.ToString());
            }
        }

        /// <summary>
        /// 简单生成，只生成类名和字段，不引用、不声明命名空间，不添加字段特性，不判断字段类型，生成文件到桌面类名.cs
        /// </summary>
        public static void Generat<T>(string classname)
        {
            var entity = Activator.CreateInstance<T>();
            entity.Generat(classname);
        }
    }
}
