using OpenSearch.Client;
using PlaygroundSolutionOS.Domain.Entities;
using PlaygroundSolutionOS.Domain.Interfaces;

namespace PlaygroundSolutionOS.Infra.Data.Repositories;

public class ProjectRepository : OpenSearchBaseRepository<Project>, IProjectRepository
{
    public override string IndexName { get; } = "projects";
    public override string Type { get; } = "project";

    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {
        return await GetAllAsync();
    }

    public async Task<Project> GetByIdAsync(string? id)
    {
        return await GetAsync(id);
    }

    public async Task<Project> CreateAsync(Project project)
    {
        await InsertAsync(project);
        return project;
    }

    public async Task<Project> UpdateAsync(Project project)
    {
        await UpdateBaseAsync(project);
        return project;
    }

    public async Task<Project> RemoveAsync(Project project)
    {
        if (project.Id != null) await DeleteByIdAsync(project.Id);
        return project;
    }

    public ProjectRepository(IOpenSearchClient openSearchClient) : base(openSearchClient)
    {
    }
}