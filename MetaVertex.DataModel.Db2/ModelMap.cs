using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaVertex.DataModel.Db2
{
    internal class ModelMap
    {
        private static readonly Dictionary<Type, ModelMap> _maps = new Dictionary<Type, ModelMap>();

        private ModelMap(Type modelType)
        {
            ModelType = modelType;
        }

        public Type ModelType { get; }
        public List<PropertyMap> Properties { get; } = new List<PropertyMap>();

        internal static ModelMap GetMap(Type modelType)
        {
            if (_maps.ContainsKey(modelType))
                return _maps[modelType];

            var map = new ModelMap(modelType);
            map.Properties.AddRange(GetPropertyMaps(modelType));

            return _maps[modelType] = map;
        }

        private static IEnumerable<PropertyMap> GetPropertyMaps(Type modelType)
        {
            foreach (var prop in modelType.GetProperties())
            {
                var attr = prop.GetCustomAttributes(typeof(DataFieldAttribute), false).Cast<DataFieldAttribute>().FirstOrDefault();

                if (attr == null)
                    continue;

                yield return new PropertyMap(prop)
                {
                    ColumnName = attr.ColumnName,
                };
            }

        }
    }
}
