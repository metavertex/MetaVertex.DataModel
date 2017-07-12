using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace MetaVertex.DataModel
{
    /// <summary>
    /// Default implementation of <see cref="IParameterRetriever{TModel}"/> which sets values directly on
    /// the model without any casting/conversion. As a special case, <see cref="DBNull"/> is converted to null.
    /// </summary>
    public class DefaultParameterRetriever<TModel> : IParameterRetriever<TModel>
    {
        /// <inheritdoc />
        public void SetModelValue(QueryPropertyMap map, DbParameter parameter, TModel model)
        {
            DefaultParameterRetriever.SetModelValue(map, parameter, model);
        }
    }

    public static class DefaultParameterRetriever
    {
        public static void SetModelValue(QueryPropertyMap map, DbParameter parameter, object model)
        {
            var value = parameter.Value;

            if (value == DBNull.Value)
                value = null;

            map.Setter.Invoke(model, new[] { value });
        }
    }
}
