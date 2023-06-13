using AutoMapper;
using PlaygroundSolutionOS.Application.DTOs;
using PlaygroundSolutionOS.Application.Interfaces;
using PlaygroundSolutionOS.Domain.Entities;
using PlaygroundSolutionOS.Domain.Interfaces;

namespace PlaygroundSolutionOS.Application.Services;

public class ProjectService : IProjectService
{
    private IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<ProjectDTO>?> GetProjects()
    {
        var projectsEntities = await _projectRepository.GetProjectsAsync();
        return _mapper.Map<IEnumerable<ProjectDTO>>(projectsEntities);
    }

    public async Task<ProjectDTO?> GetById(string? id)
    {
        var projectEntity = await _projectRepository.GetByIdAsync(id);
        return _mapper.Map<ProjectDTO>(projectEntity);
    }

    public async Task Add(ProjectDTO projectDto)
    {
        var projectEntity = _mapper.Map<Project>(projectDto);
        await _projectRepository.CreateAsync(projectEntity);
    }

    public async Task Update(ProjectDTO projectDto)
    {
        var projectEntity = _mapper.Map<Project>(projectDto);
        await _projectRepository.UpdateAsync(projectEntity);
    }

    public async Task Remove(string? id)
    {
        var projectEntity = _projectRepository.GetByIdAsync(id).Result;
        await _projectRepository.RemoveAsync(projectEntity);
    }
}