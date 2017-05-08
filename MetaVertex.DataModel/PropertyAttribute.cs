using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MetaVertex.DataModel
{
    /// <summary>
    /// Contains information about a property on a POCO model, and an instance of a <typeparamref name="TAttribute"/>
    /// attribute on that property.
    /// </summary>
    /// <typeparam name="TAttribute">The type of attribute specified on the property.</typeparam>
    public class PropertyAttribute<TAttribute> : PropertyAttribute
        where TAttribute : Attribute
    {
        public new TAttribute Attribute { get; set; }

        public void Deconstruct(out PropertyInfo propertyInfo, out TAttribute attribute)
        {
            propertyInfo = PropertyInfo;
            attribute = Attribute;
        }
    }

    /// <summary>
    /// Contains information about a property on a POCO model, and an instance of an attribute on that property.
    /// </summary>
    public class PropertyAttribute
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
