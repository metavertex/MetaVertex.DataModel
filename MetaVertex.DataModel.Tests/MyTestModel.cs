using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaVertex.DataModel.Tests
{
    public class MyTestModel
    {
        [DataField("NAME")]
        public string Name { get; set; }

        [DataField("START_DATE")]
        public DateTime StartDate { get; set; }

        [DataField("ITEM_COUNT")]
        public int ItemCount { get; set; }

        [DataField("ITEM_COST")]
        public decimal Cost { get; set; }

        public string NonMappedValue { get; set; }
    }
}
