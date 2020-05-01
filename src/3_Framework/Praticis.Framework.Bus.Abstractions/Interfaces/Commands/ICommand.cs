
using System;

using MediatR;

namespace Praticis.Framework.Bus.Abstractions.Commands
{
    /// <summary>
    /// An order of execution that togetther the necessary information to be executed.
    /// </summary>
    public interface ICommand : ICommand<bool>, IWork
    {

    }

    /// <summary>
    /// An order of execution that gather necessary information to be executed.
    /// </summary>
    /// <typeparam name="TResponse">Represents the result of command execution.</typeparam>
    public interface ICommand<TResponse> : IRequest<TResponse>
    {
        /// <summary>
        /// The Command Id that Identify Execution Order of Command
        /// </summary>
        Guid CommandId { get; }

        /// <summary>
        /// The Id of Root Entity involved in Command Execution.
        /// </summary>
        Guid? AggregateId { get; }

        /// <summary>
        /// The command name.
        /// </summary>
        string CommandName { get; }

        /// <summary>
        /// The Time execution of Command
        /// </summary>
        DateTime Time { get; }

        /// <summary>
        /// The assembly context of the Command.
        /// </summary>
        Type ResourceName { get; }
    }
}