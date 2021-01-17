using System;
using System.Collections.Generic;
using System.Text;

namespace Sneakybox_mandatory_task
{
    interface INumberIterator<T> : IEnumerator<T>
    {
        T GetNumber(T number);
    }
}
