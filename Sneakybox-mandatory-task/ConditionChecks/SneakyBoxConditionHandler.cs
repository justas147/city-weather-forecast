using Sneakybox_mandatory_task.CommunicationStrategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sneakybox_mandatory_task.ConditionChecks
{
    class SneakyBoxConditionHandler : BaseHandler<int>
    {
        public SneakyBoxConditionHandler(ICommunicationStrategy communicationService) : base(communicationService) 
        { }
        
        // This method handles the main test and passes control to the next handler
        public override void Handle(int number)
        {
            if (number % 3 == 0 && number % 5 == 0)
            {
                communicationService.SendMessage("SneakyBox");
            }
            else if (number % 3 == 0)
            {
                communicationService.SendMessage("Sneaky");
            }
            else if (number % 5 == 0)
            {
                communicationService.SendMessage("Box");
            }
            else
            {
                communicationService.SendMessage(number.ToString());
            }

            base.Handle(number);
        }
    }
}
