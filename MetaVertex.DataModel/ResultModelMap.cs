using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MetaVertex.DataModel
{
    internal class ResultModelMap : ModelMapBase<ResultModelMap, ResultPropertyMap, DataColumnAttribute>
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

            return map;
        }

        private static void ApplyAttributes(Type modelType, ResultModelMap map)
        {
            var attr = modelType.GetTypeInfo().GetCustomAttribute<DataModelAttribute>();

            if (attr == null)
                return;

            map.AutoTrim = attr.AutoTrim;
        }

        /// <inheritdoc />
        protected override ResultPropertyMap CreatePropertyMap(PropertyInfo propInfo, DataColumnAttribute attr)
        {
            var map = new ResultPropertyMap(propInfo, attr);

            if (AutoTrim || attr.AutoTrim)
                map.Modifiers.Add(AutoTrimValueModifier.Instance);

            return map;
        }

        internal static void ClearCache()
        {
            ClearCacheImpl();
        }

    }
}
