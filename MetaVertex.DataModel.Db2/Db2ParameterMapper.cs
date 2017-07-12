using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2.iSeries;

namespace MetaVertex.DataModel.Db2
{
    public class Db2ParameterMapper<TModel> : IParameterCreator<TModel, iDB2Parameter>, IParameterRetriever<TModel, iDB2Parameter>
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

        /// <inheritdoc />
        public virtual void SetModelValue(QueryPropertyMap map, iDB2Parameter parameter, TModel model)
        {
            DefaultParameterRetriever.SetModelValue(map, parameter, model);
        }
    }

    public static class Db2ParameterMapper
    {
        public static Db2ParameterMapper<TModel> ForModel<TModel>(TModel model)
        {
            return new Db2ParameterMapper<TModel>();
        }
    }
}