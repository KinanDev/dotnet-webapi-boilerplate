using AutoMapper;

using FSH.WebApi.Application.Catalog.Activities;

namespace FSH.WebApi.Application.Mapper;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateActivityRequest, Activity>().ReverseMap();
    }
}
