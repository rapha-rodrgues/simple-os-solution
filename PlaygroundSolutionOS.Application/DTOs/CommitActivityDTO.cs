using System.Runtime.Serialization;

namespace PlaygroundSolutionOS.Application.DTOs;

// ReSharper disable once InconsistentNaming
public class CommitActivityDTO
{
    public string? Id { get; set; }
    public DeveloperDTO? Committer { get; set; }
    public double ConfidenceFactor { get; set; }
    public string? Message { get; set; }
    public long SizeInBytes { get; set; }
}
