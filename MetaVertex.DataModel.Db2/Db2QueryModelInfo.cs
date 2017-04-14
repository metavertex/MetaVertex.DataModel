using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2.iSeries;

namespace MetaVertex.DataModel.Db2
{
    public class Db2QueryModelInfo<TModel> : QueryModelInfo<TModel, iDB2Parameter>
    {
        /// <inheritdoc />
        public Db2QueryModelInfo(TModel model)
            : base(model)
        {
        }

        /// <inheritdoc />
        protected override iDB2Parameter CreateParameter(QueryPropertyMap map)
        {
            var parm = new iDB2Parameter(map.ParameterName, map.GetValue(Model))
            {
                Direction = map.ParameterDirection,
            };

            return parm;
        }
    }

    public static class Db2QueryModelInfo
    {
        public static Db2QueryModelInfo<TModel> Create<TModel>(TModel model)
        {
            return new Db2QueryModelInfo<TModel>(model);
        }
    }
}
