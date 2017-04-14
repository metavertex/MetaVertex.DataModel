using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaVertex.DataModel
{
    /// <summary>
    /// Identifies a property on a data model which will have its value mapped from the
    /// specified result set field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DataColumnAttribute : DataItemAttributeBase
    {
        /// <inheritdoc />
        public DataColumnAttribute()
        {
        }

        /// <inheritdoc />
        public DataColumnAttribute(string name)
            : base(name)
        {
        }

        /// <inheritdoc />
        public DataColumnAttribute(int index)
            : base(index)
        {
        }

        /// <summary>
        /// If true, values on this field will be trimmed.
        /// </summary>
        public bool AutoTrim { get; set; }
    }
}