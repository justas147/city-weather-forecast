using System;

namespace Sneakybox_mandatory_task.CommunicationStrategies
{
    class ConsoleCommunication : ICommunicationStrategy
    {
        public void SendMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
