

using Shared.Library.Model.Pagination;

namespace Shared.Library.Interface;

public interface  IGenericRepository<T> where T: class
{
    Task<T?> GetByIdAsync<TRes>(Guid id);
    Task<PagedResult<TRes>>GetAllAsync<TRes>(QueryParameters queryParameters);

    Task<TRes?> CreateAsync <TSource,TRes>(TSource entity);
    Task<TRes> UpdateAsync<TSource, TRes>(int id, TSource entity)
        where TSource : class
        where TRes : class;
    Task<bool> DeleteAsync(int id);
    Task<bool> Exists(int id);
        
}
