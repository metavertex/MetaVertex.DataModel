using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetaVertex.DataModel;

namespace MetaVertex.DataModel.Db2
{
    public static class DataModelExtensions
    {
        public static DataModelInfo<T> GetModelInfo<T>(this DbDataReader reader)
            where T : new()
        {
            return new DataModelInfo<T>(reader, r => new T());
        }

        public static DataModelInfo<T> GetModelInfo<T>(this DbDataReader reader, Func<DbDataReader, T> creator)
        {
            return new DataModelInfo<T>(reader, creator);
        }
    }

}
