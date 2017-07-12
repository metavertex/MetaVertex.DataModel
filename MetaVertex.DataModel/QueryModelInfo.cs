using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace MetaVertex.DataModel
{
    /// <summary>
    /// Converts models of the specified type to/from a collection of <typeparamref name="TParameter"/>s.
    /// </summary>
    public class QueryModelInfo<TModel, TParameter>
        where TParameter : DbParameter
    {
        private readonly Lazy<QueryModelMap> _map = new Lazy<QueryModelMap>(() => QueryModelMap.GetMap(typeof(TModel)));

        /// <inheritdoc />
        public QueryModelInfo(TModel model)
        {
            Model = model;
        }

        /// <summary>
        /// Returns the parameters corresponding to the values of <see cref="Model"/> by calling the
        /// <see cref="IParameterCreator{TModel, TParameter}.CreateParameter(QueryPropertyMap, TModel)"/> method
        /// on <see cref="ParameterCreator"/>.
        /// </summary>
        public IEnumerable<TParameter> GetParameters()
        {
            var creator = ParameterCreator ??
                throw new InvalidOperationException($"Must set {nameof(ParameterCreator)} before creating parameters.");

            return Map.GetOrderedMaps().Select(map => (TParameter)(map.DbParameter = creator.CreateParameter(map, Model)));
        }

        /// <summary>
        /// For any parameters created on a previous call to <see cref="GetParameters"/>, sets the values on
        /// <see cref="Model"/> for any whose <see cref="DbParameter.Direction"/> is <see cref="ParameterDirection.Output"/>
        /// or <see cref="ParameterDirection.InputOutput"/>, by calling <see cref="ParameterRetriever"/>.
        /// </summary>
        public void SetQueryResult()
        {
            var retriever = ParameterRetriever ??
                throw new InvalidOperationException(
                    $"Must set {nameof(ParameterRetriever)} before retrieving parameter values.");

            foreach (var map in Map.GetOrderedMaps().Where(map => map.DbParameter != null &&
                (map.DbParameter.Direction == ParameterDirection.Output || map.DbParameter.Direction == ParameterDirection.InputOutput)))
            {
                retriever.SetModelValue(map, (TParameter)map.DbParameter, Model);
            }
        }

        public TModel Model { get; }

        internal QueryModelMap Map => _map.Value;

        public IParameterCreator<TModel, TParameter> ParameterCreator { get; set; }

        // TODO combine IParameterCreator / IParameterRetriever into IParameterMapper
        public IParameterRetriever<TModel, TParameter> ParameterRetriever { get; set; } =
            new DefaultParameterRetriever<TModel>();
    }

    /// <summary>
    /// Converts models of the specified type to/from a collection of <see cref="DbParameter"/>s, using a method specified
    /// at runtime.
    /// </summary>
    public class QueryModelInfo<TModel> : QueryModelInfo<TModel, DbParameter>
    {
        public QueryModelInfo(TModel model)
            : base(model)
        {
        }

    }

}