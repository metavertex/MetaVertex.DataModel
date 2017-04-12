using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaVertex.DataModel
{
    /// <summary>
    /// Describes a means to modify the value returned from the data reader before it's set on the model.
    /// </summary>
    internal interface IValueModifier
    {
        /// <summary>
        /// Optionally modifies the specified value from the data reader based on the specified info, or
        /// returns it as-is.
        /// </summary>
        /// <param name="value">The value as read from the database or as modified from previous <see cref="IValueModifier"/>s.</param>
        /// <param name="fieldInfo">Metadata about the current field.</param>
        object ModifyValue(object value, ReaderFieldInfo fieldInfo);
    }
}
