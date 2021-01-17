using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Sneakybox_mandatory_task
{
    /// <summary>
    /// Base collection class which stores the collection in a data array
    /// </summary>
    /// <typeparam name="T"></typeparam>
    abstract class BaseCollection<T> : IEnumerable<T>
    {
        protected T[] _collection;

        public BaseCollection(T[] collection)
        {
            _collection = collection;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // This method is implemented by different collections to return created custom interators
        public abstract IEnumerator<T> GetEnumerator();
    }
}
