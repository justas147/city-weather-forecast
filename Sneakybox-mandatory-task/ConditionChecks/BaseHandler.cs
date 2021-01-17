using Sneakybox_mandatory_task.CommunicationStrategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sneakybox_mandatory_task.ConditionChecks
{
    abstract class BaseHandler<T> : IConditionCheckHandler<T>
    {
        protected readonly ICommunicationStrategy communicationService;
        IConditionCheckHandler<T> nextHandler;

        protected BaseHandler(ICommunicationStrategy communicationService)
        {
            this.communicationService = communicationService;
        }

        public IConditionCheckHandler<T> SetNext(IConditionCheckHandler<T> handler)
        {
            nextHandler = handler;

            return handler;
        }

        public virtual void Handle(T number)
        {
            if (nextHandler != null)
            {
                this.nextHandler.Handle(number);
            }
        }
    }
}
