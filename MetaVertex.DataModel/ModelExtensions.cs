using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaVertex.DataModel
{
    public static class ModelExtensions
    {
        public static ResultModelInfo<T> GetModelInfo<T>(this DbDataReader reader)
            where T : new()
        {
            return new ResultModelInfo<T>(reader, r => new T());
        }

        public static ResultModelInfo<T> GetModelInfo<T>(this DbDataReader reader, Func<DbDataReader, T> creator)
        {
            return new ResultModelInfo<T>(reader, creator);
        }
    }

}
