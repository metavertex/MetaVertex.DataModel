using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaVertex.DataModel
{
    /// <summary>
    /// Creates models of the specified type from a data reader instance.
    /// </summary>
    /// <typeparam name="TModel">The type of model for which data will be extracted from the data reader.</typeparam>
    public class ResultModelInfo<TModel>
    {
        private List<ReaderFieldInfo> _infos;
        private readonly Func<DbDataReader, TModel> _creator;

        public ResultModelInfo(DbDataReader reader, Func<DbDataReader, TModel> creator)
        {
            DataReader = reader ?? throw new ArgumentNullException(nameof(reader));
            _creator = creator ?? throw new ArgumentNullException(nameof(creator));
        }

        public DbDataReader DataReader { get; }

        public TModel GetModel()
        {
            var model = _creator(DataReader);

            foreach (var fieldInfo in Infos)
            {
                ApplyFieldInfo(model, fieldInfo);
            }

            return model;
        }

        private IEnumerable<ReaderFieldInfo> Infos
        {
            get
            {
                var map = ResultModelMap.GetMap(typeof(TModel));
                return _infos ?? (_infos = new List<ReaderFieldInfo>(ReaderFieldInfo.GetInfos(map, DataReader)));
            }
        }

        private void ApplyFieldInfo(TModel model, ReaderFieldInfo fieldInfo)
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
