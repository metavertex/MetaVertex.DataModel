using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaVertex.DataModel
{
    internal abstract class ModelMapBase<TModelMap, TPropertyMap>
        where TModelMap: ModelMapBase<TModelMap, TPropertyMap>
        where TPropertyMap : PropertyMapBase
    {
        private static readonly Dictionary<Type, TModelMap> _mapCache = new Dictionary<Type, TModelMap>();

        protected static void ClearCacheImpl()
        {
            _mapCache.Clear();
        }

        protected ModelMapBase(Type modelType)
        {
            ModelType = modelType;
        }

        public Type ModelType { get; }
        public List<TPropertyMap> Properties { get; } = new List<TPropertyMap>();

        public bool AutoTrim { get; set; }

        protected static TModelMap GetMapImpl(Type modelType, Func<Type, TModelMap> creator)
        {
            if (_mapCache.ContainsKey(modelType))
                return _mapCache[modelType];

            return _mapCache[modelType] = creator(modelType);
        }

    }
}