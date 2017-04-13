using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MetaVertex.DataModel
{
    internal static class ModelTools
    {
        public static IEnumerable<PropertyAttribute<TAttribute>> GetTypePropertyAttributes<TAttribute, TModel>()
            where TAttribute : Attribute
        {
            return GetTypePropertyAttributes<TAttribute>(typeof(TModel));
        }

        public static IEnumerable<PropertyAttribute<TAttribute>> GetTypePropertyAttributes<TAttribute>(Type modelType)
            where TAttribute : Attribute
        {
            foreach (var prop in modelType.GetProperties())
            {
                var attr = prop.GetCustomAttributes(typeof(TAttribute), false)
                    .Cast<TAttribute>()
                    .FirstOrDefault();

                if (attr == null)
                    continue;

                yield return new PropertyAttribute<TAttribute>
                {
                    Attribute = attr,
                    PropertyInfo = prop,
                };
            }
        }
    }
}
