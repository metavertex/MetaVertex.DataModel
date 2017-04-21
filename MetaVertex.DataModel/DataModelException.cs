using System;
using System.Collections.Generic;
using System.Text;

namespace MetaVertex.DataModel
{
    /// <summary>
    /// Exception thrown when an error occurs trying to convert a DataModel to/from its corresponding
    /// database object.
    /// </summary>
    public class DataModelException : Exception
    {
        private const string GeneralDataModelError = "An error occurred trying to convert the DataModel." +
            " See InnerException for more details.";

        public DataModelException(Exception innerException)
            : this(GeneralDataModelError, innerException)
        {
        }

        public DataModelException(string message)
            : base(message)
        {
        }

        public DataModelException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public int ErrorCount { get; internal set; }
    }
}