using PlaygroundSolutionOS.Application.DTOs;

namespace PlaygroundSolutionOS.Application.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectDTO>?> GetProjects();
    Task<ProjectDTO?> GetById(string? id);
    
    Task Add(ProjectDTO projectDto);
    Task Update(ProjectDTO projectDto);
    Task Remove(string? id);
}