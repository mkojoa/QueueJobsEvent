
using Coravel.Events.Interfaces;

namespace QueueJobsEvent.Events
{
    public class AccountCreated : IEvent
    {
        public string Message { get; set; }

        public AccountCreated(string message)
        {
            this.Message = message;
        }
    }
}