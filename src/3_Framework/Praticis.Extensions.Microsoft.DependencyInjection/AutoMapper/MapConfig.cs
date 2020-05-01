
using AutoMapper;

using Praticis.Framework.Worker.Abstractions;
using Praticis.Framework.Worker.Application.ViewModels;

namespace Praticis.Extensions.Microsoft.DependencyInjection.AutoMapper
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