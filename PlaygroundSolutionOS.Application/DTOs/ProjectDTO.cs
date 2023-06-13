using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;

namespace PlaygroundSolutionOS.Application.DTOs;

// ReSharper disable once InconsistentNaming
public class ProjectDTO
{
    public DateTime? UpdateTime { get; set; }
    public IEnumerable<string>? Branches { get; set; }
    public IList<TagDTO>? CuratedTags { get; set; }
    public string? DateString { get; set; }
    public string? Description { get; set; }

    public DateTime LastActivity { get; set; }
    public DeveloperDTO? LeadDeveloper { get; set; }
    
    [Required(ErrorMessage = "The Name is Required")]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Name { get; set; }
    public int? NumberOfCommits { get; set; }
    public int NumberOfContributors { get; set; }
    
    public DateTime StartedOn { get; set; }
    public IEnumerable<TagDTO>? Tags { get; set; }
}