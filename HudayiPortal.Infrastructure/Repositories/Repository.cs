using HudayiPortal.Domain.Repositories;
using HudayiPortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HudayiPortal.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
	private readonly ApplicationDbContext _context;
	private readonly DbSet<T> _dbSet;

	public Repository(ApplicationDbContext context)
	{
		_context = context;
		_dbSet = context.Set<T>();
	}

	public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
	{
		return await _dbSet.FindAsync([id], cancellationToken);
	}

	public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
	}

	public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
	{
		return _dbSet.Where(predicate).AsNoTracking();
	}

	public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
	{
		return await _dbSet.AnyAsync(predicate, cancellationToken);
	}

	public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
	{
		await _dbSet.AddAsync(entity, cancellationToken);
	}

	public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
	{
		await _dbSet.AddRangeAsync(entities, cancellationToken);
	}

	public void Update(T entity)
	{
		_dbSet.Update(entity);
	}

	public void Remove(T entity)
	{
		_dbSet.Remove(entity);
	}

	public void RemoveRange(IEnumerable<T> entities)
	{
		_dbSet.RemoveRange(entities);
	}
}