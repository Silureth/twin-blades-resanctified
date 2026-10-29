// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Collections.PListEnumerator`1
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace PSS.GameEngine.Collections
{
  public sealed class PListEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
  {
    private int m_index;
    private PList<T> m_collection;
    private Predicate<T> m_match;

    public void Init(PList<T> collection)
    {
      this.m_collection = collection;
      this.m_index = -1;
      this.m_match = (Predicate<T>) null;
    }

    public void Init(PList<T> collection, Predicate<T> match)
    {
      this.m_collection = collection;
      this.m_index = -1;
      this.m_match = match;
    }

    public void Reset() => this.m_index = -1;

    public bool MoveNext()
    {
      ++this.m_index;
      if (this.m_match != null)
      {
        while (this.m_index < this.m_collection.Count && !this.m_match(this.m_collection[this.m_index]))
          ++this.m_index;
      }
      return this.m_index < this.m_collection.Count;
    }

    object IEnumerator.Current => (object) this.m_collection[this.m_index];

    public T Current => this.m_collection[this.m_index];

    public void Dispose()
    {
      this.m_collection = (PList<T>) null;
      this.m_match = (Predicate<T>) null;
      EnumeratorPool<T>.Instance.Push(this);
    }
  }
}
