using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetaVertex.DataModel.Db2
{
    public sealed class DataFieldAttribute : Attribute
    {
        public DataFieldAttribute(int index)
        {
            if (index < 1)
                throw new ArgumentException("Index must not be negative");

            Index = index;
        }

        public DataFieldAttribute(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentException("Column name cannot be null or empty", nameof(columnName));

            ColumnName = columnName;
        }

        public int Index { get; }

        public string ColumnName { get; }

        /// <summary>
        /// If true, values on this field will be trimmed.
        /// </summary>
        public bool AutoTrim { get; set; }
    }
}