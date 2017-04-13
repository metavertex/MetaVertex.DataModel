using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace MetaVertex.DataModel
{
    /// <summary>
    /// Identifies a property on a model which will be mapped to a <see cref="DbParameter"/>
    /// on a <see cref="DbCommand"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ParameterValueAttribute : Attribute
    {
        public string ParameterName { get; set; }

        public int Index { get; set; }

        public ParameterDirection ParameterDirection { get; set; } = ParameterDirection.Input;
    }
}
