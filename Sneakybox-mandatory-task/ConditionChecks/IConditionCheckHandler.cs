using System;
using System.Collections.Generic;
using System.Text;

namespace Sneakybox_mandatory_task.ConditionChecks
{
    interface IConditionCheckHandler<T>
    {
        IConditionCheckHandler<T> SetNext(IConditionCheckHandler<T> handler);
        void Handle(T number);
    }
}
