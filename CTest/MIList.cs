using System;
using System.Collections;
using System.Collections.Generic;

namespace CTest
{
    public class MIList<T> : IList<T> where T : struct
    {
        T curr;
        MIList<T> next;
        int _count;

        public MIList(params T[] t)
        {
            if (t.Length == 0)
            {
                _count = 0;
            }
            else
            {
                _count = t.Length;
                curr = t[0];
                if (t.Length > 1)
                {
                    T[] tsub = new T[t.Length - 1];
                    for (var i = 0; i < tsub.Length; i++)
                    {
                        tsub[i] = t[i + 1];
                    }
                    next = new MIList<T>(tsub);
                }
            }
        }

        public T this[int index]
        {
            get
            {
                var temp = this;
                for (var i = 0; i < index; i++)
                {
                    temp = temp.next;
                }
                return temp.curr;
            }
            set
            {
                this[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public void Add(T item)
        {
            var temp = this;
            while (temp.next != null)
            {
                temp = temp.next;
            }
            temp.next = new MIList<T>(item);
            _count++;
        }

        public void Clear()
        {
            curr = default(T);
            next = null;
            _count = 0;
            GC.Collect();//垃圾回收,回收没有引用的next.next及next.next.next....
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var temp = this;
            while (temp.next != null)
            {
                yield return temp.curr;
                temp = temp.next;
            }
            //yield break;
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

}
