using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MetaVertex.DataModel
{
    public abstract class PropertyMapBase
    {
        internal PropertyMapBase(PropertyInfo info)
        {
            PropInfo = info ?? throw new ArgumentNullException(nameof(info));

            Getter = info.GetMethod;
            Setter = info.SetMethod;
        }

        public PropertyInfo PropInfo { get; }

        public string PropertyName => PropInfo.Name;

        public Type PropertyType => PropInfo.PropertyType;

        public MethodInfo Getter { get; }

        public MethodInfo Setter { get; }
    }
}
