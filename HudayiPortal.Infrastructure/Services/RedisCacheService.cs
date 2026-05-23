using HudayiPortal.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HudayiPortal.Infrastructure.Services;

public class RedisCacheService : ICacheService
{
	private readonly IDistributedCache _distributedCache;
	private readonly IMemoryCache _memoryCache;
	private readonly ILogger<RedisCacheService> _logger;

	public RedisCacheService(
		IDistributedCache distributedCache, 
		IMemoryCache memoryCache, 
		ILogger<RedisCacheService> logger)
	{
		_distributedCache = distributedCache;
		_memoryCache = memoryCache;
		_logger = logger;
	}

	public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
	{
		try
		{
			var cachedData = await _distributedCache.GetStringAsync(key, cancellationToken);
			if (cachedData != null)
			{
				return JsonSerializer.Deserialize<T>(cachedData);
			}
		}
		catch (Exception ex)
		{
			_logger.LogWarning("Redis bağlantı hatası alındı. Bellek içi (InMemory) önbellek kullanılıyor. Detay: {Message}", ex.Message);
			
			if (_memoryCache.TryGetValue(key, out T? value))
			{
				return value;
			}
		}

		return default;
	}

	public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
	{
		var expiry = expiration ?? TimeSpan.FromHours(1);

		try
		{
			var options = new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = expiry
			};

			var serializedData = JsonSerializer.Serialize(value);
			await _distributedCache.SetStringAsync(key, serializedData, options, cancellationToken);
			return;
		}
		catch (Exception ex)
		{
			_logger.LogWarning("Redis bağlantı hatası alındı. Veri Bellek içi (InMemory) önbelleğe yazılıyor. Detay: {Message}", ex.Message);
		}

		// Fallback to IMemoryCache
		_memoryCache.Set(key, value, expiry);
	}

	public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
	{
		try
		{
			await _distributedCache.RemoveAsync(key, cancellationToken);
			return;
		}
		catch (Exception ex)
		{
			_logger.LogWarning("Redis bağlantı hatası alındı. Bellek içi (InMemory) önbellekten siliniyor. Detay: {Message}", ex.Message);
		}

		// Fallback to IMemoryCache
		_memoryCache.Remove(key);
	}
}
