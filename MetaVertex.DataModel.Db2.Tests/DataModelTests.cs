using System;
using System.Linq;
//using IBM.Data.DB2.iSeries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetaVertex.DataModel.Db2.Tests
{
    [TestClass]
    public class DataModelTests
    {
        [TestMethod]
        public void CreateModelMap()
        {
            var type = typeof(MyTestModel);
            var map = ModelMap.GetMap(type);

            Assert.IsNotNull(map);
            Assert.AreEqual(type, map.ModelType);
            Assert.AreEqual(4, map.Properties.Count);

            Assert.IsTrue(HasPropertyType(map, nameof(MyTestModel.Name), "NAME", typeof(string)));
            Assert.IsTrue(HasPropertyType(map, nameof(MyTestModel.StartDate), "START_DATE", typeof(DateTime)));
            Assert.IsTrue(HasPropertyType(map, nameof(MyTestModel.ItemCount), "ITEM_COUNT", typeof(int)));
            Assert.IsTrue(HasPropertyType(map, nameof(MyTestModel.Cost), "ITEM_COST", typeof(decimal)));
        }

        private bool HasPropertyType(ModelMap map, string propertyName, string columnName, Type type)
        {
            return map.Properties.Any(p => p.ColumnName == columnName && p.PropertyName == propertyName && p.PropertyType == type);
        }

        //[TestMethod]
        public void CreateModelFromDataReader()
        {
            // TODO mock & test iDB2DataReader here
        }
    }
}
