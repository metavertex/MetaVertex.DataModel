using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MetaVertex.DataModel.Tests
{
    public class DataModelTests
    {
        [Fact]
        public void CreateResultModelMap()
        {
            var type = typeof(TestResultModel);
            var map = GetRequiredResultModelMap(type);
            Assert.Equal(type, map.ModelType);
            Assert.Equal(4, map.PropertyMaps.Count);

            bool HasMatchingProperty(string propertyName, string columnName, Type propertyType)
            {
                return map.PropertyMaps.Any(p => p.ColumnName == columnName && p.PropertyName == propertyName && p.PropertyType == propertyType);
            }

            Assert.True(HasMatchingProperty(nameof(TestResultModel.Name), "NameField", typeof(string)));
            Assert.True(HasMatchingProperty(nameof(TestResultModel.StartDate), "BeginDate", typeof(DateTime)));
            Assert.True(HasMatchingProperty(nameof(TestResultModel.ItemCount), nameof(TestResultModel.ItemCount), typeof(int)));
            Assert.True(HasMatchingProperty(nameof(TestResultModel.Cost), "ITEM_COST", typeof(decimal)));
        }

        [Fact]
        public void PropertyMapHasAutoTrimModifier()
        {
            var map = GetRequiredResultModelMap(typeof(TestResultModel));

            var propMap = GetRequiredPropertyMap(map.PropertyMaps, nameof(TestResultModel.Name));

            Assert.Equal(1, propMap.Modifiers.Count(m => m is AutoTrimValueModifier));

            propMap = GetRequiredPropertyMap(map.PropertyMaps, nameof(TestResultModel.StartDate));
            Assert.Equal(0, propMap.Modifiers.Count(m => m is AutoTrimValueModifier));
        }

        [Fact]
        public void CreateQueryModelMap()
        {
            var type = typeof(TestQueryModel);
            var map = GetRequiredQueryModelMap(type);
            Assert.Equal(type, map.ModelType);
            Assert.Equal(5, map.PropertyMaps.Count);

            bool HasMatchingProperty(string propertyName, string parameterName, int index)
            {
                return map.PropertyMaps.Any(p => p.PropertyName == propertyName && p.ParameterName == parameterName
                    && p.Index == index);
            }

            Assert.True(HasMatchingProperty(nameof(TestQueryModel.Name), "Name", 0));
        }

        private ResultModelMap GetRequiredResultModelMap(Type modelType)
        {
            var map = ResultModelMap.GetMap(modelType);

            Assert.NotNull(map);

            return map;
        }

        private QueryModelMap GetRequiredQueryModelMap(Type modelType)
        {
            var map = QueryModelMap.GetMap(modelType);

            Assert.NotNull(map);

            return map;
        }

        private T GetRequiredPropertyMap<T>(IEnumerable<T> properties, string propertyName)
            where T : PropertyMapBase
        {
            var map = properties.SingleOrDefault(p => p.PropertyName == propertyName);

            Assert.NotNull(map);

            return map;
        }

    }
}
