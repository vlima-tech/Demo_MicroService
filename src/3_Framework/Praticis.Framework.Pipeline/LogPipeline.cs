
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Newtonsoft.Json;

using Praticis.Framework.Bus.Abstractions;
using Praticis.Framework.Bus.Abstractions.Commands;
using Praticis.Framework.Bus.Abstractions.ValueObjects;
using Praticis.Framework.Pipeline.Abstractions;
using Praticis.Framework.Pipeline.Abstractions.Events;

namespace Praticis.Framework.Pipeline
{
    public class LogPipeline<TCommand, TResponse> : IPipelineBehavior<TCommand, TResponse>
    {
        private readonly IServiceBus _serviceBus;
        private readonly IPipelineStore _pipelineStore;
        
        public LogPipeline(IServiceProvider serviceProvider)
        {
            this._serviceBus = serviceProvider.GetService<IServiceBus>();
            this._pipelineStore = serviceProvider.GetService<IPipelineStore>();
        }

        public async Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            string pipelineLog;
            bool pipelineInitialized;
            int commandHash;
            StringBuilder builder = new StringBuilder();
            PipelineInfo pipeline;

            commandHash = command.GetHashCode();
            pipelineInitialized = this._pipelineStore.HasPipeline();

            builder.AppendLine(this.GeneratePipelineInput(command, pipelineInitialized));

            pipelineLog = builder.ToString();
            this._pipelineStore.Add(new PipelineInfo(commandHash, pipelineLog));

            var result = await next();

            pipeline = this._pipelineStore.GetPipeline(commandHash);

            foreach (var pipelineInfo in this._pipelineStore)
            {
                if (pipeline.IsInsideOf(pipelineInfo))
                    builder.AppendLine(pipelineInfo.Value);
            }

            builder.AppendLine(this.GeneratePipelineOutput(command, pipelineInitialized));

            pipelineLog = builder.ToString();

            this._pipelineStore.Add(new PipelineInfo(commandHash, pipelineLog));

            if (!pipelineInitialized)
            {
                await this._serviceBus.PublishEvent(new PipelineFinishedEvent(command as ICommand, this._serviceBus.Notifications, pipelineLog));
                this._pipelineStore.Clear();
            }

            return result;
        }

        private string GeneratePipelineInput(TCommand command, bool pipelineInitialized)
        {
            StringBuilder builder = new StringBuilder();
            string commandName;

            commandName = command.GetType().Name;

            if (!pipelineInitialized)
                builder.AppendLine($"\n------------------------------------------- Starting Command: {commandName} ----------------------------------------------\n");
            else
                builder.AppendLine($"\n\nStarting Command: {commandName}\n");

            builder.AppendLine("------------");
            builder.AppendLine("Input");
            builder.AppendLine("------------");

            builder.AppendLine(JsonConvert.SerializeObject(command));

            return builder.ToString();
        }

        private string GeneratePipelineOutput(TCommand command, bool pipelineInitialized)
        {
            StringBuilder builder = new StringBuilder();
            string logLine, commandName;
            int commandHash;

            commandHash = command.GetHashCode();
            commandName = command.GetType().Name;

            builder.AppendLine("\n------------");
            builder.AppendLine("Output ");
            builder.AppendLine("------------ \n");

            if (!this._serviceBus.Notifications.HasNotifications())
                builder.AppendLine("Command executed successfully \n");
            else
                builder.AppendLine("Command executed with errors \n");

            var notifications = this.ObtainsCommandNotifications(commandHash);

            foreach (var notification in notifications)
            {
                logLine = string.Format("{0} [{1}]: Code: {2}; Message: {3} ",
                    notification.Time, notification.NotificationType, notification.Code, notification.Value);

                builder.AppendLine(logLine);
            }

            builder.AppendLine(this.GenerateLogRuntime(command));

            var pipeline = this._pipelineStore.GetPipeline(commandHash);

            var pipelineFooter = $"Finishing Command: {commandName} ({pipeline.Elapsed.TotalMilliseconds} ms)";

            if (!pipelineInitialized)
                builder.AppendLine($"\n------------------------------------------- {pipelineFooter} ----------------------------------------------\n");
            else
                builder.AppendLine($"\n{pipelineFooter}\n");

            return builder.ToString();
        }

        private string GenerateLogRuntime(TCommand command)
        {
            StringBuilder builder = new StringBuilder();
            int commandHash;

            commandHash = command.GetHashCode();

            builder.AppendLine("\n\n------------");
            builder.AppendLine("Runtime ");
            builder.AppendLine("------------ \n");

            builder.AppendLine("Elapsed: " + this._pipelineStore.GetPipeline(commandHash).Elapsed.ToString());

            return builder.ToString();
        }

        private IEnumerable<Notification> ObtainsCommandNotifications(int commandHash)
        {
            List<Notification> notifications = new List<Notification>();
            PipelineInfo pipeline;
            int pipelineIndex;

            pipeline = this._pipelineStore.GetPipeline(commandHash);

            pipelineIndex = this._pipelineStore.IndexOf(pipeline);

            notifications = this._serviceBus.Notifications.Find(p => p.Time >= pipeline.StartTime && p.Time <= pipeline.FinishTime)
                .ToList();

            if (pipelineIndex == this._pipelineStore.Count - 1)
            {
                notifications.RemoveAll(n => n.Time < this._pipelineStore[pipelineIndex].StartTime && n.Time > this._pipelineStore[pipelineIndex].FinishTime);

                return notifications;
            }

            for (int i = pipelineIndex + 1; i < this._pipelineStore.Count; i++)
                notifications.RemoveAll(n => n.Time >= this._pipelineStore[i].StartTime && n.Time <= this._pipelineStore[i].FinishTime);

            return notifications;
        }
    }
}