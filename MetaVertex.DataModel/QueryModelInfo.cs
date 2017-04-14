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
    public abstract class QueryModelInfo<TModel, TParameter> : QueryModelInfoBase<TModel>
        where TParameter : DbParameter
    {
        protected QueryModelInfo(TModel model)
            : base(model)
        {
        }

        public IEnumerable<TParameter> GetParameters()
        {
            return Map.GetOrderedMaps().Select(CreateParameter);
        }

        protected abstract TParameter CreateParameter(QueryPropertyMap map);
    }

    /// <summary>
    /// Converts models of the specified type to/from a collection of <see cref="DbParameter"/>s, using a method specified
    /// at runtime.
    /// </summary>
    public class QueryModelInfo<TModel> : QueryModelInfoBase<TModel>
    {
        /// <inheritdoc />
        public QueryModelInfo(TModel model)
            : base(model)
        {
        }

        public IEnumerable<DbParameter> GetParameters()
        {
            var creator = ParameterCreator ??
                throw new InvalidOperationException($"Must set {nameof(ParameterCreator)} before creating parameters.");

            return Map.GetOrderedMaps().Select(creator);
        }

        public Func<QueryPropertyMap, DbParameter> ParameterCreator { get; set; }

    }

    public abstract class QueryModelInfoBase<TModel>
    {
        private readonly Lazy<QueryModelMap> _map = new Lazy<QueryModelMap>(() => QueryModelMap.GetMap(typeof(TModel)));

        protected QueryModelInfoBase(TModel model)
        {
            Model = model;
        }

        public TModel Model { get; }

        internal QueryModelMap Map => _map.Value;

        protected void SetParameterValues(QueryPropertyMap map, DbParameter parm)
        {
            parm.Value = map.GetValue(Model);
            parm.Direction = map.ParameterDirection;
        }
    }
}