// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Collections.EnumeratorPool`1
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

namespace PSS.GameEngine.Collections
{
  public sealed class EnumeratorPool<T>
  {
    private const int CAPACITY = 4;
    private static volatile EnumeratorPool<T> s_instance;
    private static object s_syncRoot = new object();
    private object m_syncRoot = new object();
    private PListEnumerator<T>[] m_pool;
    private int m_top;

    public static EnumeratorPool<T> Instance
    {
      get
      {
        if (EnumeratorPool<T>.s_instance == null)
        {
          lock (EnumeratorPool<T>.s_syncRoot)
          {
            if (EnumeratorPool<T>.s_instance == null)
              EnumeratorPool<T>.s_instance = new EnumeratorPool<T>();
          }
        }
        return EnumeratorPool<T>.s_instance;
      }
    }

    private EnumeratorPool()
    {
      this.m_pool = new PListEnumerator<T>[4];
      for (int index = 0; index < 4; ++index)
        this.m_pool[index] = new PListEnumerator<T>();
      this.m_top = 4;
    }

    public PListEnumerator<T> Pop()
    {
      lock (this.m_syncRoot)
      {
        --this.m_top;
        return this.m_pool[this.m_top];
      }
    }

    public void Push(PListEnumerator<T> enumerator)
    {
      lock (this.m_syncRoot)
      {
        this.m_pool[this.m_top] = enumerator;
        ++this.m_top;
      }
    }
  }
}
