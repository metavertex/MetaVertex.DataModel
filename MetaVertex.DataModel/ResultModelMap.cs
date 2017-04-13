using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MetaVertex.DataModel
{
    internal class ResultModelMap : ModelMapBase<ResultModelMap, ResultPropertyMap>
    {
        /// <inheritdoc />
        private ResultModelMap(Type modelType)
            : base(modelType)
        {
        }

        internal static ResultModelMap GetMap(Type modelType)
        {
            return GetMapImpl(modelType, CreateMap);
        }

        private static ResultModelMap CreateMap(Type modelType)
        {
            var map = new ResultModelMap(modelType);
            ApplyAttributes(modelType, map);
            map.Properties.AddRange(GetPropertyMaps(map, modelType));

            return map;
        }

        private static void ApplyAttributes(Type modelType, ResultModelMap map)
        {
            var attr = modelType.GetTypeInfo().GetCustomAttribute<DataModelAttribute>();

            if (attr == null)
                return;

            map.AutoTrim = attr.AutoTrim;
        }

        private static IEnumerable<ResultPropertyMap> GetPropertyMaps(ResultModelMap modelMap, Type modelType)
        {
            foreach (var (prop, attr) in ModelTools.GetTypePropertyAttributes<DataFieldAttribute>(modelType))
            {
                var map = new ResultPropertyMap(prop)
                {
                    ColumnName = attr.ColumnName,
                };

                var trimmer = new Lazy<AutoTrimValueModifier>(() => new AutoTrimValueModifier());

                if (modelMap.AutoTrim || attr.AutoTrim)
                    map.Modifiers.Add(trimmer.Value);

                yield return map;
            }
        }

        internal static void ClearCache()
        {
            ClearCacheImpl();
        }
    }
}
