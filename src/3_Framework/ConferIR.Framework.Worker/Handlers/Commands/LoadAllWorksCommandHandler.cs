
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Praticis.Framework.Bus.Abstractions.Commands;
using Praticis.Framework.Worker.Abstractions.Repositories;
using Praticis.Framework.Worker.Application.Commands;
using Praticis.Framework.Worker.Application.ViewModels;

namespace Praticis.Framework.Worker.Handlers.Commands
{
    public class LoadAllWorksCommandHandler : ICommandHandler<LoadAllWorksCommand, List<WorkViewModel>>
    {
        private readonly IMapper _mapper;
        private IWorkRepository _workRepository { get; set; }

        public LoadAllWorksCommandHandler(IServiceProvider serviceProvider)
        {
            this._workRepository = serviceProvider.GetService<IWorkRepository>();
            this._mapper = serviceProvider.GetService<IMapper>();
        }

        public async Task<List<WorkViewModel>> Handle(LoadAllWorksCommand request, CancellationToken cancellationToken)
        {
            var works = await _workRepository.GetAllAsync(1, request.Limit);

            var worksViewModel = _mapper.Map<List<WorkViewModel>>(works);

            return worksViewModel;
        }

        public void Dispose()
        {
            this._workRepository?.Dispose();
            this._workRepository = null;
        }
    }
}