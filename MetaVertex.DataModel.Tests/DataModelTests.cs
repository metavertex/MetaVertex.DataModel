using System;
using System.Linq;
using Xunit;

namespace MetaVertex.DataModel.Tests
{
    public class DataModelTests
    {
        [Fact]
        public void CreateModelMap()
        {
            var type = typeof(MyTestModel);
            var map = ResultModelMap.GetMap(type);

            Assert.NotNull(map);
            Assert.Equal(type, map.ModelType);
            Assert.Equal(4, map.Properties.Count);

            Assert.True(HasPropertyType(map, nameof(MyTestModel.Name), "NAME", typeof(string)));
            Assert.True(HasPropertyType(map, nameof(MyTestModel.StartDate), "START_DATE", typeof(DateTime)));
            Assert.True(HasPropertyType(map, nameof(MyTestModel.ItemCount), "ITEM_COUNT", typeof(int)));
            Assert.True(HasPropertyType(map, nameof(MyTestModel.Cost), "ITEM_COST", typeof(decimal)));
        }

        private bool HasPropertyType(ResultModelMap map, string propertyName, string columnName, Type type)
        {
            return map.Properties.Any(p => p.ColumnName == columnName && p.PropertyName == propertyName && p.PropertyType == type);
        }

        //[Fact]
        public void CreateModelFromDataReader()
        {
            // TODO mock & test DbDataReader here
        }
    }
}
