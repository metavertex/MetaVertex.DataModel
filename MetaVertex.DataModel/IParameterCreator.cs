using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace MetaVertex.DataModel
{
    /// <summary>
    /// Creates a DbParameter from a value on a QueryModel of type <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TModel">
    /// The type of the QueryModel whose values will be mapped to/from <see cref="TParameter"/> instances.
    /// </typeparam>
    /// <typeparam name="TParameter">
    /// The type of <see cref="DbParameter"/> derived parameter which will be mapped to/from <see cref="TModel"/>.
    /// </typeparam>
    public interface IParameterCreator<in TModel, out TParameter>
        where TParameter : DbParameter
    {
        /// <summary>
        /// Called to create a <see cref="DbParameter"/> from the specified mapping values and model.
        /// If the method returns null, a <see cref="DataModelException"/> will be thrown indicating
        /// that the parameter could not be created.
        /// </summary>
        TParameter CreateParameter(QueryPropertyMap map, TModel model);
    }

    /// <summary>
    /// Creates a DbParameter from a value on a QueryModel of type <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TModel">
    /// The type of the QueryModel whose values will be mapped to/from DbParameter instances.
    /// </typeparam>
    public interface IParameterCreator<in TModel> : IParameterCreator<TModel, DbParameter>
    {
    }

}
