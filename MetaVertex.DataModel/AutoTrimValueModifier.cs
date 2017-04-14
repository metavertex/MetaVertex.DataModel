using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaVertex.DataModel
{
    internal class AutoTrimValueModifier : IValueModifier
    {
        public static AutoTrimValueModifier Instance => SingletonFactory<AutoTrimValueModifier>.GetSingleton();

        /// <inheritdoc />
        public object ModifyValue(object value, ReaderFieldInfo fieldInfo)
        {
            if (fieldInfo.PropertyMap.PropertyType != typeof(string))
                return value;

            return (value as string)?.TrimEnd();
        }
    }
}
