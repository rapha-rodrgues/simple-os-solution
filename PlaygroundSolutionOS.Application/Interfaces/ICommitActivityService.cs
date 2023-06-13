using PlaygroundSolutionOS.Application.DTOs;

namespace PlaygroundSolutionOS.Application.Interfaces;

public interface ICommitActivityService
{
    Task<IEnumerable<CommitActivityDTO>?> GetCommits(string projectName);
    Task<CommitActivityDTO?> GetById(string? id);
    
    Task Add(string projectName, CommitActivityDTO commitDto);
    Task Update(CommitActivityDTO commitDto);
    Task Remove(string? id);
}