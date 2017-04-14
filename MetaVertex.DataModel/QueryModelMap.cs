using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MetaVertex.DataModel
{
    internal class QueryModelMap : ModelMapBase<QueryModelMap, QueryPropertyMap, ParameterValueAttribute>
    {
        /// <inheritdoc />
        private QueryModelMap(Type modelType)
            : base(modelType)
        {
        }

        internal static QueryModelMap GetMap(Type modelType)
        {
            return GetMapImpl(modelType, CreateMap);
        }

        private static QueryModelMap CreateMap(Type modelType)
        {
            var map = new QueryModelMap(modelType);

            return map;
        }

        /// <inheritdoc />
        protected override QueryPropertyMap CreatePropertyMap(PropertyInfo propInfo, ParameterValueAttribute attr)
        {
            var map = new QueryPropertyMap(propInfo, attr)
            {
                ParameterDirection = attr.ParameterDirection,
                DbType = attr.DbType,
            };

            return map;
        }
    }
}
