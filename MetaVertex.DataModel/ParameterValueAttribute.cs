using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace MetaVertex.DataModel
{
    /// <summary>
    /// Identifies a property on a model which will be mapped to a <see cref="DbParameter"/>
    /// on a <see cref="DbCommand"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ParameterValueAttribute : DataItemAttributeBase
    {
        /// <inheritdoc />
        public ParameterValueAttribute()
        {
        }

        /// <inheritdoc />
        public ParameterValueAttribute(string name)
            : base(name)
        {
        }

        /// <inheritdoc />
        public ParameterValueAttribute(int index)
            : base(index)
        {
        }

        public ParameterDirection ParameterDirection { get; set; } = ParameterDirection.Input;

        public DbType DbType { get; set; }
    }
}
