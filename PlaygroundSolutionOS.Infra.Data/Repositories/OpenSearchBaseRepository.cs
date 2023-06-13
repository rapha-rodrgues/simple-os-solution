using Serilog;
using OpenSearch.Client;
using PlaygroundSolutionOS.Domain.Entities;
using PlaygroundSolutionOS.Infra.Data.Interfaces;

namespace PlaygroundSolutionOS.Infra.Data.Repositories;


public abstract class OpenSearchBaseRepository<T> : IOpenSearchBaseRepository<T> where T : BaseEntity
{
    private readonly IOpenSearchClient _osClient;
    public abstract string IndexName { get; }
    public abstract string Type { get; }
    
    protected OpenSearchBaseRepository(IOpenSearchClient openSearchClient)
    {
        _osClient = openSearchClient;
    }
    public async Task<T?> GetAsync(string id)
    {
        if (id == null) throw new ArgumentNullException(nameof(id));
        var response = await _osClient.GetAsync(
            DocumentPath<T>.Id(id).Index(IndexName));

        if (response.IsValid)
            return response.Source;

        Log.Error(response.OriginalException, "{Error}",response.ServerError?.ToString()!);
        return null;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var query = new QueryContainerDescriptor<T>().Term("type", Type);
        var search = new SearchDescriptor<T>(IndexName)
            .Query(q => query);
        var response = await _osClient.SearchAsync<T>(search);

        if (!response.IsValid)
            throw new Exception(response.ServerError?.ToString(), response.OriginalException);

        return response.Hits.Select(hit => hit.Source).ToList();
    }

    public async Task<bool> InsertAsync(T model)
    {
        var response = await _osClient.CreateAsync(model, descriptor => descriptor
                .Index(IndexName));

        if (!response.IsValid)
            throw new Exception(response.ServerError?.ToString(), response.OriginalException);

        return true;
    }

    public async Task<bool> UpdateBaseAsync(T model)
    {
        var response = await _osClient.UpdateAsync(DocumentPath<T>.Id(model.Id)
            .Index(IndexName), p => p.Doc(model));

        if (!response.IsValid)
            throw new Exception(response.ServerError?.ToString(), response.OriginalException);

        return true;
    }

    public async Task<long> GetTotalCountAsync()
    {
        var search = new SearchDescriptor<T>(IndexName).MatchAll();
        var response = await _osClient.SearchAsync<T>(search);

        if (!response.IsValid)
            throw new Exception(response.ServerError?.ToString(), response.OriginalException);

        return response.Total;
    }

    public async Task<bool> DeleteByIdAsync(string id)
    {
        var response = await _osClient.DeleteAsync(DocumentPath<T>.Id(id).Index(IndexName));

        if (!response.IsValid)
            throw new Exception(response.ServerError?.ToString(), response.OriginalException);

        return true;
    }
    
    public async Task<IEnumerable<T>> SearchAsync(Func<QueryContainerDescriptor<T>, QueryContainer> request)
    {
        var response = await _osClient.SearchAsync<T>(s =>
            s.Index(IndexName)
                .Query(request));

        if (!response.IsValid)
            throw new Exception(response.ServerError?.ToString(), response.OriginalException);

        return response.Hits.Select(hit => hit.Source).ToList();
    }
}