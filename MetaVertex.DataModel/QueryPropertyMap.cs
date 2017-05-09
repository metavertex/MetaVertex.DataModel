using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

        /// <summary>
        /// The DbParameter which was created to map this model value.
        /// </summary>
        internal DbParameter DbParameter { get; set; }
    }
}