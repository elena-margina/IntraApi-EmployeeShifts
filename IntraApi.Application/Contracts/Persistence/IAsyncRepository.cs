using System.Linq.Expressions;

namespace IntraApi.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T?> GetByPredicateAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAllAsync(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task UpdateAsync(T entity, params Expression<Func<T, object>>[] updatedProperties);
        Task DeleteAsync(T entity);
        Task<IReadOnlyList<T>> ExecuteStoredProcedureAsync(string storedProcedureName, object parameters = null);
        Task ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, object parameters = null);
    }
}
