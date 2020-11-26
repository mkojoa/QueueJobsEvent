
using System;
using System.Threading.Tasks;
using Coravel.Events.Interfaces;
using QueueJobsEvent.Events;

namespace QueueJobsEvent.Listeners
{
    public class NotifyAccountByEmailListener : IListener<AccountCreated>
    {
        public async Task HandleAsync(AccountCreated broadcasted)
        {
            Console.Write(broadcasted.Message + "Started...");
            await Task.Delay(15000);
            Console.Write(broadcasted.Message + "Done.");
        }
    }
}