using System;
using System.Collections;
using System.Collections.Generic;

namespace Resanctified.Engine.Collections
{
    public sealed class PList<T> : List<T>, IEnumerable
    {
        public PList()
        {
        }

        public PList(IEnumerable<T> collection)
          : base(collection)
        {
        }

        public PList(int capacity)
          : base(capacity)
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            PListEnumerator<T> enumerator = EnumeratorPool<T>.Instance.Pop();
            enumerator.Init(this);
            return enumerator;
        }

        public IEnumerator<T> GetEnumerator(Predicate<T> match)
        {
            PListEnumerator<T> enumerator = EnumeratorPool<T>.Instance.Pop();
            enumerator.Init(this, match);
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            PListEnumerator<T> enumerator = EnumeratorPool<T>.Instance.Pop();
            enumerator.Init(this);
            return enumerator;
        }
    }
}
