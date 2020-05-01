
using System.Collections.Generic;

using Praticis.Framework.Bus.Abstractions.ValueObjects;
using Praticis.Framework.Worker.Application.ViewModels;

namespace Praticis.Framework.Worker.Application.Commands
{
    public class LoadAllWorksCommand : Command<List<WorkViewModel>>
    {
        public int Limit { get; private set; }

        public LoadAllWorksCommand(int limit = 10)
        {
            this.Limit = limit;
        }
    }
}