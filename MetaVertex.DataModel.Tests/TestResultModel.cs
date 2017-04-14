using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaVertex.DataModel.Tests
{
    public class TestResultModel
    {
        [DataColumn("NameField", AutoTrim = true)]
        public string Name { get; set; }

        [DataColumn("BeginDate")]
        public DateTime StartDate { get; set; }

        [DataColumn]
        public int ItemCount { get; set; }

        [DataColumn("ITEM_COST")]
        public decimal Cost { get; set; }

        public string NonMappedValue { get; set; }
    }
}
