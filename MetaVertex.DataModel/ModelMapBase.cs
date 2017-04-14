using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MetaVertex.DataModel
{
    internal abstract class ModelMapBase<TModelMap, TPropertyMap, TPropertyAttribute>
        where TModelMap: ModelMapBase<TModelMap, TPropertyMap, TPropertyAttribute>
        where TPropertyMap : PropertyMapBase
        where TPropertyAttribute : Attribute
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

        private void CreatePropertyMaps()
        {
            Properties.AddRange(GetPropertyMaps());
        }

        private IEnumerable<TPropertyMap> GetPropertyMaps()
        {
            foreach (var item in ModelTools.GetTypePropertyAttributes<TPropertyAttribute>(ModelType))
                yield return CreatePropertyMap(item.PropertyInfo, item.Attribute);
        }

        /// <summary>
        /// Creates a <see cref="TPropertyMap"/> property map using the specified <see cref="PropertyInfo"/> and
        /// <see cref="TPropertyAttribute"/> attribute.
        /// </summary>
        protected abstract TPropertyMap CreatePropertyMap(PropertyInfo propInfo, TPropertyAttribute attr);

        protected void ApplyAttributeToPropertyMap(DataItemAttributeBase attr, TPropertyMap map, PropertyInfo info)
        {
            
        }

        protected static TModelMap GetMapImpl(Type modelType, Func<Type, TModelMap> creator)
        {
            if (_mapCache.ContainsKey(modelType))
                return _mapCache[modelType];

            var map = creator(modelType);
            map.CreatePropertyMaps();

            return _mapCache[modelType] = map;
        }

    }
}