
using System;

using Praticis.Framework.Bus.Abstractions.Enums;
using Praticis.Framework.Bus.Abstractions.ValueObjects;

namespace Praticis.Framework.Worker.Application.Commands
{
    public class TestCommand : Command
    {
        public TimeSpan SleepInterval { get; private set; }

        public TestCommand(TimeSpan sleepInterval = default)
            : base(ExecutionMode.Enqueue)
        {
            this.SleepInterval = sleepInterval;
        }
    }
}