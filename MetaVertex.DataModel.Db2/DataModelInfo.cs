using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.Data.DB2.iSeries;

namespace MetaVertex.DataModel.Db2
{
    /// <summary>
    /// Contains the metadata necessary to map strongly-typed models from a data reader.
    /// </summary>
    /// <typeparam name="T">The type of model for which data will be extracted from the data reader.</typeparam>
    public class DataModelInfo<T>
    {
        private readonly List<ReaderFieldInfo> _infos;
        private readonly Func<iDB2DataReader, T> _creator;

        public DataModelInfo(iDB2DataReader reader, Func<iDB2DataReader, T> creator)
        {
            DataReader = reader ?? throw new ArgumentNullException(nameof(reader));
            _creator = creator ?? throw new ArgumentNullException(nameof(creator));

            var map = ModelMap.GetMap(typeof(T));

            _infos = new List<ReaderFieldInfo>(ReaderFieldInfo.GetInfos(map, reader));
        }

        public iDB2DataReader DataReader { get; }

        public T GetModel()
        {
            var model = _creator(DataReader);

            foreach (var fieldInfo in _infos)
            {
                ApplyFieldInfo(model, fieldInfo);
            }

            return model;
        }

        private void ApplyFieldInfo(T model, ReaderFieldInfo fieldInfo)
        {
            var value = DataReader.GetValue(fieldInfo.ColumnIndex);

            value = fieldInfo.PropertyMap.Modifiers.Aggregate(value,
                (current, modifier) => modifier.ModifyValue(current, fieldInfo));

            try
            {
                fieldInfo.PropertyMap.Setter.Invoke(model, new[] { value });
            }
            catch (ArgumentException ex)
            {
                const string msg = "Could not set property '{0}' to value '{1}' from column '{2}' of type '{3}' ({4})";

                throw new InvalidOperationException(string.Format(msg, fieldInfo.PropertyMap.PropertyName, value,
                    fieldInfo.PropertyMap.ColumnName, DataReader.GetFieldType(fieldInfo.ColumnIndex),
                    DataReader.GetDataTypeName(fieldInfo.ColumnIndex)), ex);
            }
        }

    }

}
