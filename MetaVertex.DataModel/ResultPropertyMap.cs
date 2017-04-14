using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MetaVertex.DataModel
{
    /// <summary>
    /// Maps a property on a model with its corresponding database column from a table or SP.
    /// </summary>
    internal class ResultPropertyMap : PropertyMapBase
    {
        internal ResultPropertyMap(PropertyInfo info, DataItemAttributeBase attr)
            : base(info, attr)
        {
            if (Setter == null)
                throw new ArgumentException($"Property '{DataItemName}' does not have public setter", nameof(info));
        }

        /// <summary>
        /// The database table or SP column name from which this property gets its value.
        /// </summary>
        public string ColumnName => DataItemName;

        public ICollection<IValueModifier> Modifiers { get; } = new List<IValueModifier>();
    }
}