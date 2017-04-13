using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MetaVertex.DataModel
{
    internal class ResultPropertyMap : PropertyMapBase
    {
        internal ResultPropertyMap(PropertyInfo info)
            : base(info)
        {
            if (Setter == null)
                throw new ArgumentException($"Property '{info.Name}' does not have public setter", nameof(info));
        }

        public string ColumnName { get; set; }

        public ICollection<IValueModifier> Modifiers { get; } = new List<IValueModifier>();
    }
}