using System;
using System.Collections.Generic;
using System.Text;

namespace MetaVertex.DataModel
{
    public abstract class DataItemAttributeBase : Attribute
    {
        protected DataItemAttributeBase()
        {
            MatchStrategy = DataItemMatchStrategy.Inferred;
        }

        protected DataItemAttributeBase(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Item name cannot be null or empty", nameof(name));

            Name = name;
            MatchStrategy = DataItemMatchStrategy.ByName;
        }

        protected DataItemAttributeBase(int index)
        {
            if (index < 0)
                throw new ArgumentException("Index must not be negative");

            Index = index;
            MatchStrategy = DataItemMatchStrategy.ByIndex;
        }

        public int Index { get; }

        public string Name { get; }

        internal DataItemMatchStrategy MatchStrategy { get; }
    }
}
