
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Praticis.Framework.Bus.Abstractions.Commands;
using Praticis.Framework.Bus.Abstractions.Events;

namespace Praticis.Framework.Bus.Abstractions
{
    public interface IServiceBus : IDisposable
    {
        /// <summary>
        /// The notification store of life cycle.
        /// </summary>
        INotificationStore Notifications { get; }

        /// <summary>
        /// Send a noitification to all handlers that signed the event publication.
        /// Generally, is a domain notification and a return is don't need.
        /// Use await Task.Run(() => PublishEvent(@event)); to parallel process or
        /// Use await PublishEvent(@event);
        /// </summary>
        /// <param name="event">The event to send.</param>
        /// <returns>
        /// Return a task running the execution.
        /// </returns>
        Task PublishEvent<TEvent>(TEvent @event) where TEvent : IEvent, IWork;

        /// <summary>
        /// Send a command to execute.
        /// Use await Task.Run(() => SendCommand(command)); to parallel process or 
        /// Use await SendCommand(command);
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>
        /// Return a task running the execution process.
        /// </returns>
        Task SendCommand<TCommand>(TCommand command) where TCommand : ICommand, IWork;

        /// <summary>
        /// Send a command to execute.
        /// Use var result = await Task.Run(() => SendCommand(command)); to parallel process or 
        /// Use var result = await SendCommand(command);
        /// </summary>
        /// <typeparam name="TResponse">The output of command.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <returns>
        /// Return the response of the command.
        /// </returns>
        Task<TResponse> SendCommand<TResponse>(ICommand<TResponse> command);

        /// <summary>
        /// Service bus add the process on stack of parallel process to be executed when possible.
        /// It's usually instantaneous.
        /// Use await QueueProcess(task);
        /// </summary>
        /// <param name="work">The task to run in parallel process.</param>
        /// <returns></returns>
        Task EnqueueWork(IWork work);

        /// <summary>
        /// Service bus add the process on stack of parallel process to be executed when possible.
        /// It's usually instantaneous.
        /// Use await QueueProcess(task);
        /// </summary>
        /// <param name="works">The tasks to run in parallel process.</param>
        /// <returns></returns>
        Task EnqueueWork(IEnumerable<IWork> works);
    }
}