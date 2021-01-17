using System;
using System.Collections.Generic;
using System.Text;

namespace Sneakybox_mandatory_task.NumberCollections
{
    class IntegerIterator : BaseIterator<int>
    {
        public IntegerIterator(int[] collection) : base(collection)
        { }

        public override int GetNumber(int number)
        {
            //apply additional logic here...

            return number;
        }
    }
}
