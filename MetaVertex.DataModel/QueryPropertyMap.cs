using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace MetaVertex.DataModel
{
    public class QueryPropertyMap : PropertyMapBase
    {
        /// <inheritdoc />
        internal QueryPropertyMap(PropertyInfo info, DataItemAttributeBase attr)
            : base(info, attr)
        {
        }

        public string ParameterName => DataItemName;

        public ParameterDirection ParameterDirection { get; set; } = ParameterDirection.Input;

        public DbType DbType { get; set; }
    }
}