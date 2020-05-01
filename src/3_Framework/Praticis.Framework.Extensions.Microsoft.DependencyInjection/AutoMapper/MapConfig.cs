
using AutoMapper;

using ConferIR.Framework.Worker.Abstractions;
using ConferIR.Framework.Worker.Application.ViewModels;

namespace ConferIR.Extensions.Microsoft.DependencyInjection.AutoMapper
{
    public class MapConfig : Profile
    {
        public MapConfig()
        {
            CreateMap<Work, WorkViewModel>()
                .ReverseMap();
        }
    }
}