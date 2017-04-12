using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MetaVertex.DataModel
{
    internal class PropertyMap
    {
        internal PropertyMap(PropertyInfo info)
        {
            PropInfo = info ?? throw new ArgumentNullException(nameof(info));

            Setter = info.SetMethod ?? throw new ArgumentException($"Property '{info.Name}' does not have public setter",
                nameof(info));
        }

        public string ColumnName { get; set; }

        public ICollection<IValueModifier> Modifiers { get; } = new List<IValueModifier>();

        public PropertyInfo PropInfo { get; }

        public MethodInfo Setter { get; }

        public string PropertyName => PropInfo.Name;

        public Type PropertyType => PropInfo.PropertyType;
    }
}
