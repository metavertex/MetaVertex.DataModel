using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MetaVertex.DataModel
{
    internal class PropertyAttribute<TAttribute> : PropertyAttribute
        where TAttribute : Attribute
    {
        public new TAttribute Attribute { get; set; }

        public void Deconstruct(out PropertyInfo propertyInfo, out TAttribute attribute)
        {
            propertyInfo = PropertyInfo;
            attribute = Attribute;
        }
    }

    internal class PropertyAttribute
    {
        public Attribute Attribute { get; set; }
        public PropertyInfo PropertyInfo { get; set; }

        public void Deconstruct(out PropertyInfo propertyInfo, out Attribute attribute)
        {
            propertyInfo = PropertyInfo;
            attribute = Attribute;
        }
    }
}
