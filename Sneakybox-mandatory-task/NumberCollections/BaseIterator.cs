using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sneakybox_mandatory_task
{
    abstract class BaseIterator<T> : INumberIterator<T>
    {
        readonly T[] _collection;
        int position;

        public BaseIterator(T[] collection)
        {
            _collection = collection;
            position = -1;
        }

        public T Current 
        {
            get
            {
                try
                {
                    return GetNumber(_collection[position]);
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current 
        {
            get
            {
                return Current;
            }
        }


        public void Dispose()
        { }

        public bool MoveNext()
        {
            position++;
            return (position < _collection.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        // This method is used when the iterator is getting the current value
        // When BaseIterator is inherited we can apply custom logic to the value returned in loop
        public abstract T GetNumber(T number);
    }
}
