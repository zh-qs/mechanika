using System;
using System.Collections.Generic;
using System.Text;

namespace MechanikaInterface.Collections
{
    public class LimitedStack<T>
    {
        private T[] tbl;
        public int Count { get; private set; }
        public int Capacity { get => tbl.Length; }
        public LimitedStack(int capacity)
        {
            tbl = new T[capacity];
            Count = 0;
        }

        public void Push(T elem) // odrzuca nadmiarowe elementy
        {
            if (Count < Capacity)
            {
                tbl[Count++] = elem;
            }
            else
            {
                MoveElements();
                tbl[Count - 1] = elem;
            }
        }

        public T Pop()
        {
            return tbl[--Count];
        }

        private void MoveElements()
        {
            for (int i=1;i<Count;i++)
                tbl[i - 1] = tbl[i];
        }

        public T[] GetAllItems()
        {
            T[] revTab = new T[Count];
            for (int i = Count - 1; i >= 0; --i)
                revTab[Count - i - 1] = tbl[i];
            return revTab;
        }
    }
}
