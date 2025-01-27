using IntraApi.Application.Contracts.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IntraApi.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly IntraApiDBContext _dbContext;

        public BaseRepository(IntraApiDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T?> GetByPredicateAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync() ?? new List<T>();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        //public async Task<IReadOnlyList<T>> ListAllAsync(Expression<Func<T, bool>> predicate)
        //{
        //    var result = await _dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
        //    return result == null ? null : new List<T> { result };
        //}

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        //public async Task UpdateAsync(T entity)
        //{
        //    var entry = _dbContext.Entry(entity);
        //    entry.State = EntityState.Modified;

        //    // Detach related entities if necessary
        //    foreach (var navigation in entry.Navigations)
        //    {
        //        if (navigation.CurrentValue != null && navigation.IsLoaded)
        //        {
        //            _dbContext.Entry(navigation.CurrentValue).State = EntityState.Detached;
        //        }
        //    }

        //    await _dbContext.SaveChangesAsync();
        //}


        public async Task UpdateAsync(T entity, params Expression<Func<T, object>>[] updatedProperties)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            foreach (var property in updatedProperties)
            {
                _dbContext.Entry(entity).Property(property).IsModified = true;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Executes a stored procedure asynchronously with parameters and returns the result as a list of the specified entity type.
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure.</param>
        /// <param name="parameters">The parameters for the stored procedure.</param>
        /// <returns>A list of the specified entity type.</returns>
        public async Task<IReadOnlyList<T>> ExecuteStoredProcedureAsync(string storedProcedureName, object parameters = null)
        {
            var sql = $"EXEC {storedProcedureName}";

            if (parameters != null)
            {
                sql += " " + string.Join(", ",
                    parameters.GetType()
                        .GetProperties()
                        .Select(p => $"@{p.Name} = {{{p.Name}}}"));
            }

            return await _dbContext.Set<T>()
                .FromSqlInterpolated($"{sql}")
                .ToListAsync();
        }

        public async Task ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, object parameters = null)
        {
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentException("Stored procedure name cannot be null or empty.", nameof(storedProcedureName));

            var sql = $"EXEC {storedProcedureName}";

            if (parameters != null)
            {
                sql += " " + string.Join(", ",
                    parameters.GetType()
                        .GetProperties()
                        .Select(p => $"@{p.Name} = {{{p.Name}}}"));
            }

            await _dbContext.Database.ExecuteSqlInterpolatedAsync($"{sql}");
        }
    }
}
