using Coravel.Invocable;
using System;
using System.Threading.Tasks;

namespace QueueJobsEvent.Invocables
{
    public class DoExpensiveCalculationAndStore : IInvocable
    {
        public DoExpensiveCalculationAndStore()
        {
        }

        public async Task Invoke()
        {
            // What is your invocable going to do?
            Console.Write("Doing expensive calculation for 15 sec...");
            await Task.Delay(15000);
            Console.Write("Expensive calculation done.");
        }
    }
}
