using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace MetaVertex.DataModel
{
    /// <summary>
    /// Converts models of the specified type to/from a collection of <see cref="DbParameter"/>s.
    /// </summary>
    public class QueryModelInfo<TModel>
    {
        public QueryModelInfo(TModel model)
        {
            Model = model;

            var map = QueryModelMap.GetMap(typeof(TModel));
        }

        public TModel Model { get; }

        public IEnumerable<DbParameter> GetParameters()
        {
            var modelType = typeof(TModel);

            foreach (var (prop, attr) in ModelTools.GetTypePropertyAttributes<ParameterValueAttribute>(modelType))
            {
                
            }

            throw new NotImplementedException();
        }
    }
}
