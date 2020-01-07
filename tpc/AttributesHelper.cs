using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace tpc
{
    public class AttributesHelper
    {
        public static PropertyInfo[] GetProperties<T>()
        {
            var type = typeof(T);
            var propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            return propertyInfos;
        }

        public static IEnumerable<IEnumerable<T>> GetFieldAttributes<Entity, T>(PropertyInfo[] propertyInfos) where Entity : new() where T : Attribute
        {
            foreach (var property in propertyInfos)
            {
                yield return property.GetCustomAttributes<T>();
            }
        }

        public static IEnumerable<IEnumerable<T>> GetFieldAttributes<Entity, T>() where Entity : new() where T : Attribute
        {
            var propertyInfos = GetProperties<Entity>();

            foreach (var property in propertyInfos)
            {
                yield return property.GetCustomAttributes<T>();
            }
        }

        public static T GetFieldAttributeDescrition<Entity, T>(PropertyInfo property) where Entity : new() where T : Attribute
        {
            var attrs = property.GetCustomAttributes<T>();
            if (attrs.Any())
            {
                return attrs.First();
            }
            return null;
        }

        public static T GetClassAttributeDescrition<Entity, T>() where Entity : new() where T : Attribute
        {
            var type = typeof(Entity);
            var attr = type.GetCustomAttribute<T>();
            return attr;
        }
    }
}
