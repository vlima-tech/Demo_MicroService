
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Praticis.Framework.Bus.Abstractions.Commands;
using Praticis.Framework.Worker.Abstractions;
using Praticis.Framework.Worker.Abstractions.Repositories;
using Praticis.Framework.Worker.Application.Commands;
using Praticis.Framework.Worker.Application.ViewModels;

namespace Praticis.Framework.Worker.Handlers.Commands
{
    public class LoadWorksCommandHandler : ICommandHandler<LoadWorksCommand, WorksPaginatedViewModel>
    {
        private readonly IMapper _mapper;
        private IWorkRepository _workRepository { get; set; }

        public LoadWorksCommandHandler(IServiceProvider serviceProvider)
        {
            this._workRepository = serviceProvider.GetService<IWorkRepository>();
            this._mapper = serviceProvider.GetService<IMapper>();
        }

        public async Task<WorksPaginatedViewModel> Handle(LoadWorksCommand request, CancellationToken cancellationToken)
        {
            var count = await _workRepository.CountAllAsync(WorkSpec.LoadWorks(request.Queue));

            var works = await _workRepository.FindAsync(
                WorkSpec.LoadWorks(request.Queue), 
                w => w.CreationDate, 
                request.PageIndex, 
                request.PageSize, 
                request.DescendingOrder
            );

            var worksViewModel = _mapper.Map<IEnumerable<WorkViewModel>>(works);

            return new WorksPaginatedViewModel(worksViewModel, count, request.PageIndex, request.PageSize);
        }

        public void Dispose()
        {
            this._workRepository?.Dispose();
            this._workRepository = null;
        }
    }
}