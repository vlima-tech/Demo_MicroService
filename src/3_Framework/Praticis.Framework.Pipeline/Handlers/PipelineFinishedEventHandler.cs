
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Praticis.Framework.Bus.Abstractions.Events;
using Praticis.Framework.Pipeline.Abstractions.Events;

namespace Praticis.Framework.Pipeline.Handlers
{
    public class PipelineFinishedEventHandler : IEventHandler<PipelineFinishedEvent>
    {
        public async Task Handle(PipelineFinishedEvent notification, CancellationToken cancellationToken)
        {
            var pipeline = notification.PipelineLog;

            if (string.IsNullOrEmpty(pipeline))
                return;

            var directory = new Uri(AppDomain.CurrentDomain.BaseDirectory)
                .Append("logs", "pipeline");

            var fileName = $"{DateTime.Now.ToString("yyyyMMdd")}_pipeline.txt";
            var path = directory.Append(fileName);
            
            if (!Directory.Exists(directory.LocalPath))
                Directory.CreateDirectory(directory.LocalPath);

            using (StreamWriter file = new StreamWriter(path.LocalPath, true))
                await file.WriteAsync(pipeline);
        }
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}