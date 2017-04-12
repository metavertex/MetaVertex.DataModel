using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace MetaVertex.DataModel.Db2
{
    /// <summary>
    /// Contains information linking the column from a data reader with the property on the model type.
    /// </summary>
    internal class ReaderFieldInfo
    {
        public static IEnumerable<ReaderFieldInfo> GetInfos(ModelMap map, DbDataReader reader)
        {
            for (var i = 0; i < reader.FieldCount; i++)
            {
                var columnName = reader.GetName(i);

                var prop = map.Properties.FirstOrDefault(p => p.ColumnName == columnName);

                if (prop != null)
                    yield return new ReaderFieldInfo
                    {
                        ColumnIndex = i,
                        PropertyMap = prop,
                    };
            }
        }

        public int ColumnIndex { get; private set; }
        public PropertyMap PropertyMap { get; private set; }
    }

}