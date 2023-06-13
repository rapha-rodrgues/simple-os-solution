using PlaygroundSolutionOS.Domain.Entities;

namespace PlaygroundSolutionOS.Domain.Interfaces;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetProjectsAsync();
    Task<Project> GetByIdAsync(string? id);
    Task<Project> CreateAsync(Project project);
    Task<Project> UpdateAsync(Project project);
    Task<Project> RemoveAsync(Project project);
}