using System;
using System.Collections.Generic;
using System.Text;

namespace MetaVertex.DataModel
{
    /// <summary>
    /// Describes how we determine which item in a database object (result set, parameter list etc) we will match
    /// to the model.
    /// </summary>
    internal enum DataItemMatchStrategy
    {
        /// <summary>
        /// Model attribute did not specify name or index. Will be inferred by model (defaults to using property name).
        /// </summary>
        Inferred = 0,

        /// <summary>
        /// Explicit database item name was specified on attribute.
        /// </summary>
        ByName,

        /// <summary>
        /// Explicit database item index was specified on attribute.
        /// </summary>
        ByIndex,
    }
}
