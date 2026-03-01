using HudayiPortal.Domain.Repositories;
using HudayiPortal.Infrastructure.Persistence;
using System.Collections.Concurrent;

namespace HudayiPortal.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
	private readonly ApplicationDbContext _context;
	private readonly ConcurrentDictionary<Type, object> _repositories = new();
	private bool _disposed;

	public UnitOfWork(ApplicationDbContext context)
	{
		_context = context;
	}

	public IRepository<T> Repository<T>() where T : class
	{
		return (IRepository<T>)_repositories.GetOrAdd(typeof(T), _ => new Repository<T>(_context));
	}

	public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		return await _context.SaveChangesAsync(cancellationToken);
	}

	public void Dispose()
	{
		if (!_disposed)
		{
			_context.Dispose();
			_disposed = true;
		}

		GC.SuppressFinalize(this);
	}
}