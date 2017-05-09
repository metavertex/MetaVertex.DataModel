using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace MetaVertex.DataModel
{
    /// <summary>
    /// Sets values on a QueryModel of type <typeparamref name="TModel"/> using DbParameters of the specified type.
    /// </summary>
    /// <typeparam name="TModel">
    /// The type of the QueryModel whose values will be set from <see cref="TParameter"/> instances.
    /// </typeparam>
    /// <typeparam name="TParameter">
    /// The type of <see cref="DbParameter"/> derived parameter which will be mapped to <see cref="TModel"/> values.
    /// </typeparam>
    public interface IParameterRetriever<in TModel, in TParameter>
        where TParameter : DbParameter
    {
        /// <summary>
        /// Called to set the value on the specified <see cref="TModel"/> from the specified mapping values and
        /// parameter.
        /// </summary>
        void SetModelValue(QueryPropertyMap map, TParameter parameter, TModel model);
    }

    /// <summary>
    /// Sets values on a QueryModel of type <typeparamref name="TModel"/> using DbParameters of the specified type.
    /// </summary>
    /// <typeparam name="TModel">
    /// The type of the QueryModel whose values will be set from <see cref="DbParameter"/> instances.
    /// </typeparam>
    public interface IParameterRetriever<in TModel> : IParameterRetriever<TModel, DbParameter>
    {
        
    }
}