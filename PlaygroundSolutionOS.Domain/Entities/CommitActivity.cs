using OpenSearch.Client;

namespace PlaygroundSolutionOS.Domain.Entities;

public class CommitActivity : BaseEntity
{
    private static string TypeName = "commit";
    public string Type => TypeName;

    private string? _projectName;

    public Developer Committer { get; set; }
    public double ConfidenceFactor { get; set; }
    public TimeSpan? Duration { get; set; }
    
    public JoinField? Join { get; set; }

    public string? Message { get; set; }

    /// <summary>
    /// This is lazy, both project and commits end up in the same index under the same type (_doc)
    /// Quite a few of our tests do script lookups based on this field under the old assumption only a specific type
    /// is searched.
    /// </summary>
    public int? NumberOfCommits { get; set; }

    public string? ProjectName
    {
        get => _projectName;
        set
        {
            Join = JoinField.Link<CommitActivity>(value);
            _projectName = value;
        }
    }

    public long SizeInBytes { get; set; }

    [Text]
    public TimeSpan? StringDuration
    {
        get => Duration;
        set => Duration = value;
    }
}