using System;
using System.Collections.Generic;
using System.Text;

namespace MetaVertex.DataModel.Tests
{
    public class TestQueryModel
    {
        [ParameterValue(0)]
        public string Name { get; set; }

        [ParameterValue(1)]
        public DateTime StartDate { get; set; }

        [ParameterValue(2)]
        public int ItemCount { get; set; }

        [ParameterValue(3)]
        public decimal Cost { get; set; }

        public string NonMappedValue { get; set; }
    }
}
