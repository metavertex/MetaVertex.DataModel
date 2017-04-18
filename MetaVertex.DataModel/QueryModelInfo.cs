using System;
using System.Collections.Generic;
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

        public IEnumerable<TParameter> GetParameters()
        {
            var creator = ParameterCreator ??
                throw new InvalidOperationException($"Must set {nameof(ParameterCreator)} before creating parameters.");

            return Map.GetOrderedMaps().Select(map => creator.CreateParameter(map, Model));
        }

        public TModel Model { get; }

        internal QueryModelMap Map => _map.Value;

        public IParameterCreator<TModel, TParameter> ParameterCreator { get; set; }
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