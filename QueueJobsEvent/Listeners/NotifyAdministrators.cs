
using System.Threading.Tasks;
using Coravel.Events.Interfaces;
using QueueJobsEvent.Events;

namespace QueueJobsEvent.Listeners
{
    public class NotifyAdministrators : IListener<AccountCreated>
    {
        public Task HandleAsync(AccountCreated broadcasted)
        {
            return Task.CompletedTask;
        }
    }
}