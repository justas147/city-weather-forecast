using System;
using System.Collections.Generic;
using System.Text;

namespace Sneakybox_mandatory_task.NumberCollections
{
    class IntegerCollection : BaseCollection<int>
    {
        public IntegerCollection(int[] collection) : base(collection)
        { }

        public override IEnumerator<int> GetEnumerator()
        {
            return new IntegerIterator(_collection);
        }
    }
}
