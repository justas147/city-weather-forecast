using System;
using System.Collections.Generic;
using System.Text;

namespace Sneakybox_mandatory_task.CommunicationStrategies
{
    interface ICommunicationStrategy
    {
        void SendMessage(string message);
    }
}
