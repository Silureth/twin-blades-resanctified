// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Collections.PList`1
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace PSS.GameEngine.Collections
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
      return (IEnumerator<T>) enumerator;
    }

    public IEnumerator<T> GetEnumerator(Predicate<T> match)
    {
      PListEnumerator<T> enumerator = EnumeratorPool<T>.Instance.Pop();
      enumerator.Init(this, match);
      return (IEnumerator<T>) enumerator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      PListEnumerator<T> enumerator = EnumeratorPool<T>.Instance.Pop();
      enumerator.Init(this);
      return (IEnumerator) enumerator;
    }
  }
}
