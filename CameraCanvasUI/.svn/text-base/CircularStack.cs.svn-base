using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CameraCanvas
{
   /// <summary>
   /// [Deprecated] Formerly used for Undo/Redo.
   /// Replaced with C# Stack to give unlimited Undoes/Redoes.
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public class CircularStack<T> : IEnumerable<T>, IEnumerator<T>, IEnumerator
    {
        private int capacity;
        private List<T> circle;
        private int pointer;
        private int index;
        private int endindex;

        public CircularStack(int capacity)
        {
            this.capacity = capacity;
            this.pointer = capacity - 1;
            this.circle = new List<T>();
            for (int i = 0; i < capacity; i++)
                circle.Add(default(T));
        }

        public void Push(T newitem)
        {


            this.pointer++;
            if (this.pointer == this.capacity)
            {
                this.pointer = 0;
            }
            Console.WriteLine("push {0}/{1}", pointer, capacity);
            this.circle[pointer] = newitem;
        }

        public T Pop()
        {
            if (this.circle[pointer] == null)
            {
                return default(T);
            }
            else
            {
                T returnitem = this.circle[pointer];
                this.circle[pointer] = default(T);
                pointer--;
                if (pointer < 0)
                {
                    pointer = capacity - 1;
                }

                Console.WriteLine("pop {0}/{1}", pointer, capacity);
                return returnitem;
            }
        }

        public T Peek()
        {
            return this.Peek(pointer);
        }

        public T Peek(int i)
        {
            T returnitem = this.circle[i];
            Console.WriteLine("peek {0}/{1}", pointer, capacity);
            return returnitem;
        }

        public int Count
        {
            get
            {
                return Math.Min(this.capacity, this.circle.Count);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < capacity; i++)
                circle[i] = default(T);
            this.pointer = this.capacity - 1;
        }


        public IEnumerator<T> GetEnumerator()
        {
            this.Reset();
            return (IEnumerator<T>)this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Dispose()
        {

        }

        public T Current
        {
            get
            {
                return this.circle[index];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.circle[index];
            }
        }


        public bool MoveNext()
        {
            this.index--;
            if (index < 0) index = this.capacity - 1;
            //Console.WriteLine("{0} / {1}", index, endindex);
            if (index == endindex || circle[index] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Reset()
        {
            this.index = this.pointer + 1;
            this.endindex = this.index;
            if (endindex == this.capacity) endindex = 0;
        }


    }
}
