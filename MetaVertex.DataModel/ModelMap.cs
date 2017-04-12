using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MetaVertex.DataModel
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

        public bool AutoTrim { get; set; }

        internal static ModelMap GetMap(Type modelType)
        {
            if (_maps.ContainsKey(modelType))
                return _maps[modelType];

            var map = new ModelMap(modelType);
            ApplyAttributes(modelType, map);

            map.Properties.AddRange(GetPropertyMaps(map, modelType));

            return _maps[modelType] = map;
        }

        private static void ApplyAttributes(Type modelType, ModelMap map)
        {
            var attr = modelType.GetTypeInfo().GetCustomAttribute<DataModelAttribute>();

            if (attr == null)
                return;

            map.AutoTrim = attr.AutoTrim;
        }

        private static IEnumerable<PropertyMap> GetPropertyMaps(ModelMap modelMap, Type modelType)
        {
            foreach (var prop in modelType.GetProperties())
            {
                var attr = prop.GetCustomAttributes(typeof(DataFieldAttribute), false).Cast<DataFieldAttribute>().FirstOrDefault();

                if (attr == null)
                    continue;

                var map = new PropertyMap(prop)
                {
                    ColumnName = attr.ColumnName,
                };

                var trimmer = new Lazy<AutoTrimValueModifier>(() => new AutoTrimValueModifier());

                if (modelMap.AutoTrim || attr.AutoTrim)
                    map.Modifiers.Add(trimmer.Value);

                yield return map;
            }

        }

    }
}
