namespace Resanctified.Engine.Tools
{
    public class Pool<T> where T : class, new()
    {
        private int m_size;
        private T[] m_data;
        private int[] m_free;
        private int m_dataCount;
        private int m_freeCount;

        public Pool(int size)
        {
            this.m_data = new T[size];
            this.m_free = new int[size];
            this.m_size = size;
            for (int index = 0; index < size; ++index)
                this.m_data[index] = new T();
        }

        public void Clear()
        {
            this.m_freeCount = 0;
            this.m_dataCount = 0;
        }

        public T Add()
        {
            T obj = this.m_freeCount != 0 ? this.m_data[this.m_free[--this.m_freeCount]] : this.m_data[this.m_dataCount];
            ++this.m_dataCount;
            return obj;
        }

        public void Remove(T item) => this.Remove(this.GetIndexOf(item));

        public void Remove(int index)
        {
            this.m_free[this.m_freeCount++] = index;
            --this.m_dataCount;
        }

        public int GetIndexOf(T item)
        {
            int indexOf = 0;
            bool flag = false;
            while (indexOf < this.m_size && !flag)
            {
                if (ReferenceEquals(this.m_data[indexOf], item))
                    flag = true;
                else
                    ++indexOf;
            }
            return indexOf;
        }

        public T Get(int index) => this.m_data[index];

        public int GetCount() => this.m_dataCount;

        public bool IsEmpty() => this.m_dataCount == 0;
    }
}
