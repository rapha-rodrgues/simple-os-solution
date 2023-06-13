using AutoMapper;
using PlaygroundSolutionOS.Application.DTOs;
using PlaygroundSolutionOS.Domain.Entities;


namespace PlaygroundSolutionOS.Application.Mappings;

public class DomainToDtoMappingProfile : Profile
{
    public DomainToDtoMappingProfile()
    {
        CreateMap<Project, ProjectDTO>().ReverseMap();
        CreateMap<Developer, DeveloperDTO>().ReverseMap();
        CreateMap<Tag, TagDTO>().ReverseMap();
        CreateMap<CommitActivity, CommitActivityDTO>().ReverseMap();
    }
}