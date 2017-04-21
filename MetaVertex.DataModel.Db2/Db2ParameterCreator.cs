using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2.iSeries;

namespace MetaVertex.DataModel.Db2
{
    public class Db2ParameterCreator<TModel> : IParameterCreator<TModel, iDB2Parameter>
    {
        /// <inheritdoc />
        public virtual iDB2Parameter CreateParameter(QueryPropertyMap map, TModel model)
        {
            var parm = new iDB2Parameter(GetParameterName(map, model), GetValue(map, model))
            {
                Direction = map.ParameterDirection,
            };

            return parm;
        }

        public virtual string GetParameterName(QueryPropertyMap map, TModel model)
        {
            return "@" + map.ParameterName;
        }

        public virtual object GetValue(QueryPropertyMap map, TModel model)
        {
            return map.GetValue(model);
        }
    }

    public static class Db2ParameterCreator
    {
        public static Db2ParameterCreator<TModel> ForModel<TModel>(TModel model)
        {
            return new Db2ParameterCreator<TModel>();
        }
    }
}