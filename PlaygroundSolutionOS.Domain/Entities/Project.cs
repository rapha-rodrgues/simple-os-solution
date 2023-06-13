namespace PlaygroundSolutionOS.Domain.Entities;

public class Project : BaseEntity
{
    private static readonly string TypeName = "project";
    public string Type => TypeName;
    public new string? Id => Name;
    public IEnumerable<string>? Branches { get; set; }
    public IList<Tag>? CuratedTags { get; set; }
    public string? DateString { get; set; }
    public string? Description { get; set; }

    public DateTime LastActivity { get; set; }
    public Developer? LeadDeveloper { get; set; }
    public string? Name { get; set; }
    public int? NumberOfCommits { get; set; }
    public int NumberOfContributors { get; set; }
    
    public DateTime StartedOn { get; set; }
    public IEnumerable<Tag>? Tags { get; set; }
}