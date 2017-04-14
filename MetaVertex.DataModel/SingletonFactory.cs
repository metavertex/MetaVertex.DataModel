using System;
using System.Collections.Generic;
using System.Text;

namespace MetaVertex.DataModel
{
    internal static class SingletonFactory<T>
    {
        private static readonly Lazy<T> _lazy = new Lazy<T>();

        public static T GetSingleton()
        {
            return _lazy.Value;
        }
    }
}
