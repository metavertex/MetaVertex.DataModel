using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2.iSeries;

namespace MetaVertex.DataModel.Db2
{
    public static class Db2ModelExtensions
    {
        public static ResultModelInfo<T> GetModelInfo<T>(this iDB2DataReader reader)
            where T : new()
        {
            return new ResultModelInfo<T>(reader, r => new T());
        }

        public static ResultModelInfo<T> GetModelInfo<T>(this iDB2DataReader reader, Func<DbDataReader, T> creator)
        {
            return new ResultModelInfo<T>(reader, creator);
        }
    }

}
