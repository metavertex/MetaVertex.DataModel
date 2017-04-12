using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaVertex.DataModel.Db2
{
    internal class AutoTrimValueModifier : IValueModifier
    {
        /// <inheritdoc />
        public object ModifyValue(object value, ReaderFieldInfo fieldInfo)
        {
            if (fieldInfo.PropertyMap.PropertyType != typeof(string))
                return value;

            return (value as string)?.TrimEnd();
        }
    }
}
