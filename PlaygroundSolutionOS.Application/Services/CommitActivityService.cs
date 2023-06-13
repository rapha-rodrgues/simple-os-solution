using AutoMapper;
using PlaygroundSolutionOS.Application.DTOs;
using PlaygroundSolutionOS.Application.Interfaces;
using PlaygroundSolutionOS.Domain.Entities;
using PlaygroundSolutionOS.Domain.Interfaces;

namespace PlaygroundSolutionOS.Application.Services;

public class CommitActivityService : ICommitActivityService
{
    private readonly ICommitActivityRepository _commitActivityRepository;
    private readonly IMapper _mapper;

    public CommitActivityService(ICommitActivityRepository commitRepository, IMapper mapper)
    {
        _commitActivityRepository = commitRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<CommitActivityDTO>?> GetCommits(string projectName)
    {
        var commitsEntities = await _commitActivityRepository.GetCommitsAsync(projectName);
        return _mapper.Map<IEnumerable<CommitActivityDTO>>(commitsEntities);
    }

    public async Task<CommitActivityDTO?> GetById(string? id)
    {
        var commitEntity = await _commitActivityRepository.GetByIdAsync(id);
        return _mapper.Map<CommitActivityDTO>(commitEntity);
    }

    public async Task Add(string projectName, CommitActivityDTO commitDto)
    {
        var commitEntity = _mapper.Map<CommitActivity>(commitDto);
        commitEntity.ProjectName = projectName;
        await _commitActivityRepository.CreateAsync(commitEntity);
    }

    public async Task Update(CommitActivityDTO commitDto)
    {
        var commitEntity = _mapper.Map<CommitActivity>(commitDto);
        await _commitActivityRepository.UpdateAsync(commitEntity);
    }

    public async Task Remove(string? id)
    {
        var commitEntity = _commitActivityRepository.GetByIdAsync(id).Result;
        await _commitActivityRepository.RemoveAsync(commitEntity);
    }
}