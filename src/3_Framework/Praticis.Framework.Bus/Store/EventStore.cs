
using System;
using System.Threading;
using System.Threading.Tasks;

using Praticis.Framework.Bus.Abstractions.Events;

namespace Praticis.Framework.Bus.Store
{
    public class EventStore : IEventStore
    {
        public EventStore(IServiceProvider provider)
        {

        }

        public async Task<bool> Store(IEvent @event)
        {
            // Implements here sending messages to micro services
            await Task.Run(() => Thread.Sleep(3000));

            return true;
        }

        public void Dispose()
        {
            
        }
    }
}