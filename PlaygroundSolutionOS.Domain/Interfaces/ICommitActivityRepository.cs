using PlaygroundSolutionOS.Domain.Entities;

namespace PlaygroundSolutionOS.Domain.Interfaces;

public interface ICommitActivityRepository
{
    Task<IEnumerable<CommitActivity>> GetCommitsAsync(string projectName);
    Task<CommitActivity> GetByIdAsync(string? id);
    Task<CommitActivity> CreateAsync(CommitActivity commit);
    Task<CommitActivity> UpdateAsync(CommitActivity commit);
    Task<CommitActivity> RemoveAsync(CommitActivity commit);
}