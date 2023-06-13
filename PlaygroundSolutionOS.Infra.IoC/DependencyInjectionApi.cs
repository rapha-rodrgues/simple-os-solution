using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenSearch.Client;
using PlaygroundSolutionOS.Application.Interfaces;
using PlaygroundSolutionOS.Application.Mappings;
using PlaygroundSolutionOS.Application.Services;
using PlaygroundSolutionOS.Domain.Entities;
using PlaygroundSolutionOS.Domain.Interfaces;
using PlaygroundSolutionOS.Infra.Data.Repositories;

namespace PlaygroundSolutionOS.Infra.IoC;

public static class DependencyInjectionApi
{
    public static IServiceCollection AddInfrastructureApi(
        this IServiceCollection services, IConfiguration configuration)
    {
        var nodeAddress = new Uri("http://localhost:9200");
        var connSettings = new ConnectionSettings(nodeAddress)
            .DefaultMappingFor<Project>(map => map
                .IndexName("projects")
                .IdProperty(p => p.Name)
                .RelationName("project"))
            .DefaultMappingFor<CommitActivity>(map => map
                .IndexName("projects")
                .RelationName("commits"))
            .DefaultMappingFor<Developer>(map => map
                .IndexName("devs")
                .Ignore(p => p.PrivateValue)
                .PropertyName(p => p.OnlineHandle, "nickname")
            );

        var client =  new OpenSearchClient(connSettings);
        
        services.AddSingleton<IOpenSearchClient>(client);
        
        // Projects
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectService, ProjectService>();

        // Commits
        services.AddScoped<ICommitActivityRepository, CommitActivityRepository>();
        services.AddScoped<ICommitActivityService, CommitActivityService>();
        
        services.AddAutoMapper(typeof(DomainToDtoMappingProfile));
        return services;
    }
}
