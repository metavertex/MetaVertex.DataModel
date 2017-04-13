using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MetaVertex.DataModel
{
    public class QueryPropertyMap : PropertyMapBase
    {
        /// <inheritdoc />
        public QueryPropertyMap(PropertyInfo info)
            : base(info)
        {
        }
    }
}