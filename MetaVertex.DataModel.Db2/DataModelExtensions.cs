using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2.iSeries;

namespace MetaVertex.DataModel.Db2
{
    public static class DataModelExtensions
    {
        public static DataModelInfo<T> GetModelInfo<T>(this iDB2DataReader reader)
            where T : new()
        {
            return new DataModelInfo<T>(reader, r => new T());
        }

        public static DataModelInfo<T> GetModelInfo<T>(this iDB2DataReader reader, Func<iDB2DataReader, T> creator)
        {
            return new DataModelInfo<T>(reader, creator);
        }
    }

}
