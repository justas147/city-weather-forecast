using System;
using System.Linq;
using Sneakybox_mandatory_task.ConditionChecks;
using Sneakybox_mandatory_task.CommunicationStrategies;
using Sneakybox_mandatory_task.NumberCollections;

namespace Sneakybox_mandatory_task
{
    /*
     * Iterator pattern is used to provide the same interface for iterating over data from different
     * sources. 
     * New collection and iterator classes can be created for different data types.
     * The new collection and iterator classes should inherit base collection and iterator classes.
     * Base collection implemetation lets us return the created custom iterator.
     * Base iterator implemetation lets us define GetNumber() method which can be used to apply
     * logic to iterated data (extract number from "370 LT" string for example).
     * 
     * To check different conditions and in different order the Chain of Responsibility pattern is
     * introduced. Different handlers can be created to check other conditions. Different order for them
     * can also be configured. Another approach would be to use a template method, however chain 
     * of resp. seems more flexible in this task.
     * 
     * Strategy pattern allows us to have different types of communication (printing message to console,
     * sending SMS...). The comunication strategy is passed to the handlers in the 
     * chain of responsibility. That way we can also have different communication strategy for different
     * conditions
     */
    class Program
    {
        static void Main()
        {
            IConditionCheckHandler<int> conditionHandler = ConfigureIntegerHandler();

            IntegerCollection numbers = new IntegerCollection(GetNumbers(1, 100));

            foreach (int number in numbers)
            {
                conditionHandler.Handle(number);
            }
        }

        static IConditionCheckHandler<int> ConfigureIntegerHandler()
        {
            IConditionCheckHandler<int> initialHandler = 
                new SneakyBoxConditionHandler(new ConsoleCommunication());

            return initialHandler;
        }

        static int[] GetNumbers(int startIndex, int length)
        {
            return Enumerable.Range(startIndex, length).ToArray();
        }
    }
}
