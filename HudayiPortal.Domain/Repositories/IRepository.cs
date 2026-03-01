using System.Linq.Expressions;

namespace HudayiPortal.Domain.Repositories;

public interface IRepository<T> where T : class
{
	Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
	Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
	IQueryable<T> Where(Expression<Func<T, bool>> predicate);
	Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
	Task AddAsync(T entity, CancellationToken cancellationToken = default);
	Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
	void Update(T entity);
	void Remove(T entity);
	void RemoveRange(IEnumerable<T> entities);
}