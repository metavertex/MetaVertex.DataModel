using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Humanizer;
using IBM.Data.DB2.iSeries;

namespace MetaVertex.DataModel.Db2.Design
{
    /// <summary>
    /// Assists with generating models for DB2 queries / objects.
    /// </summary>
    public class Db2ModelDesigner : IDisposable
    {
        private readonly iDB2Connection _conn;

        public Db2ModelDesigner(string connString)
        {
            _conn = new iDB2Connection(connString);
        }

        /// <summary>
        /// Returns the C# definition for a POCO model suitable for storing results from the specified query.
        /// </summary>
        public string GenerateResultModel(string query)
        {
            var build = new StringBuilder();

            using (var cmd = new iDB2Command(query, _conn))
            {
                _conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    var info = new DbReaderInfo(reader);

                    foreach (var col in info.GetModelInfos())
                    {
                        build.Append(col);
                    }
                }

                _conn.Close();
            }

            return build.ToString();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _conn?.Dispose();
        }
    }

    public class DbReaderInfo
    {
        private readonly DbDataReader _reader;

        public DbReaderInfo(DbDataReader reader)
        {
            if (!reader.CanGetColumnSchema())
                throw new InvalidOperationException("The specified DbDataReader does not provide column schema info");
            _reader = reader;
        }

        public IEnumerable<DbColumnModelInfo> GetModelInfos()
        {
            return _reader.GetColumnSchema().Select(col => new DbColumnModelInfo(col));
        }
    }

    public class DbColumnModelInfo
    {
        private readonly DbColumn _col;
        internal DbColumnModelInfo(DbColumn col)
        {
            _col = col ?? throw new ArgumentNullException(nameof(col));
        }

        public string ColumnName => _col.ColumnName;
        public int? ColumnIndex => _col.ColumnOrdinal;

        public string PropertyName => ColumnName.ToLowerInvariant().Pascalize();
        public string PropertyTypeName => GetTypeName();
        public Type PropertyType => _col.DataType;
        public bool IsNullable => _col.AllowDBNull ?? true;

        private string GetTypeName()
        {
            var name = GetCsharpName(PropertyType.Name);

            if (PropertyType.IsValueType && IsNullable)
                name += "?";

            return name;
        }

        private string GetCsharpName(string typeName)
        {
            switch (typeName)
            {
                case "String":
                    return "string";
                case "Decimal":
                    return "decimal";
                case "Int32":
                    return "int";
                case "Int64":
                    return "long";
            }

            return typeName;
        }

        public override string ToString()
        {
            var build = new StringBuilder();

            build.AppendLine($"[DataColumn(\"{ColumnName}\")]");
            build.AppendLine($"public {PropertyTypeName} {PropertyName} {{ get; set; }}");
            build.AppendLine("");

            return build.ToString();
        }
    }
}
