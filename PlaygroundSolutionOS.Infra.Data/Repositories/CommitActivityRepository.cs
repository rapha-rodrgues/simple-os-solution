using OpenSearch.Client;
using PlaygroundSolutionOS.Domain.Entities;
using PlaygroundSolutionOS.Domain.Interfaces;

namespace PlaygroundSolutionOS.Infra.Data.Repositories;

public class CommitActivityRepository : OpenSearchBaseRepository<CommitActivity>, ICommitActivityRepository
{
    public override string IndexName { get; } = "projects";
    public override string Type { get; } = "commit";

    public async Task<IEnumerable<CommitActivity>> GetCommitsAsync(string projectName)
    {
        var query = new QueryContainerDescriptor<CommitActivity>().Term(t =>
            t.Field(f => f.ProjectName).Value(projectName));
        return await SearchAsync(_ => query);
    }

    public async Task<CommitActivity> GetByIdAsync(string? id)
    {
        return await GetAsync(id);
    }

    public async Task<CommitActivity> CreateAsync(CommitActivity commit)
    {
        await InsertAsync(commit);
        return commit;
    }

    public async Task<CommitActivity> UpdateAsync(CommitActivity commit)
    {
        await UpdateBaseAsync(commit);
        return commit;
    }

    public async Task<CommitActivity> RemoveAsync(CommitActivity commit)
    {
        if (commit.Id != null) await DeleteByIdAsync(commit.Id);
        return commit;
    }

    public CommitActivityRepository(IOpenSearchClient openSearchClient) : base(openSearchClient)
    {
    }
}