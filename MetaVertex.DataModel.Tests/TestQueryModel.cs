using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MetaVertex.DataModel.Tests
{
    public class TestQueryModel
    {
        [ParameterValue(0)]
        public string Name { get; set; }

        [ParameterValue(1)]
        public DateTime StartDate { get; set; }

        [ParameterValue(2, ParameterDirection = ParameterDirection.InputOutput)]
        public int ItemCount { get; set; }

        [ParameterValue(3)]
        public decimal Cost { get; set; }

        [ParameterValue(4, ParameterDirection = ParameterDirection.Output)]
        public string Result { get; set; }

        public string NonMappedValue { get; set; }
    }
}
