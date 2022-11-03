using System;

namespace My.Collections
{
    public interface ICollection
    {
        int Count { get; }
        void Clear();
   }
}