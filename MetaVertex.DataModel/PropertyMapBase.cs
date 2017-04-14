using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MetaVertex.DataModel
{
    /// <summary>
    /// Maps a property on a model with its corresponding database object.
    /// </summary>
    public abstract class PropertyMapBase
    {
        internal PropertyMapBase(PropertyInfo info, DataItemAttributeBase attr)
        {
            PropInfo = info ?? throw new ArgumentNullException(nameof(info));

            if (attr == null)
                throw new ArgumentNullException(nameof(attr));

            MatchStrategy = attr.MatchStrategy;

            Getter = info.GetMethod;
            Setter = info.SetMethod;

            DataItemName = attr.Name ?? PropertyName;
            Index = attr.Index;
        }

        public PropertyInfo PropInfo { get; }

        public string PropertyName => PropInfo.Name;

        protected string DataItemName { get; }

        public int Index { get; }

        public Type PropertyType => PropInfo.PropertyType;

        public object GetValue(object model)
        {
            return Getter.Invoke(model, null);
        }

        public void SetValue(object model, object value)
        {
            Setter.Invoke(model, new[] { value });
        }

        public MethodInfo Getter { get; }

        public MethodInfo Setter { get; }

        internal DataItemMatchStrategy MatchStrategy { get; }

        public override string ToString()
        {
            return $"{GetType().Name}: {PropertyName}";
        }
    }
}
