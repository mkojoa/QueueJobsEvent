using Coravel.Invocable;
using System.Threading.Tasks;

namespace QueueJobsEvent.Invocables
{
    public class SendDailyReportsEmailJob : IInvocable
    {
        public SendDailyReportsEmailJob()
        {
        }

        public async Task Invoke()
        {
            // What is your invocable going to do?
            await Task.Delay(15000);
        }
    }
}
