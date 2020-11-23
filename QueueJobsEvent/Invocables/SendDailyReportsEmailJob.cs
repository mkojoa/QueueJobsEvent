using Coravel.Invocable;
using System.Threading.Tasks;

namespace QueueJobsEvent.Invocables
{
    public class SendDailyReportsEmailJob : IInvocable
    {
        public SendDailyReportsEmailJob()
        {
        }

        public Task Invoke()
        {
            // What is your invocable going to do?
        }
    }
}
