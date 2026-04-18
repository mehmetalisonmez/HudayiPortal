using HudayiPortal.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HudayiPortal.Infrastructure.Services;

public sealed class LocalFileService : IFileService
{
	private readonly IWebHostEnvironment _env;

	public LocalFileService(IWebHostEnvironment env)
	{
		_env = env;
	}

	public async Task<string> UploadFileAsync(IFormFile file, string subFolder)
	{
		if (file is null || file.Length == 0)
			throw new ArgumentException("Dosya seçilmedi veya boþ.");

		// Use WebRootPath if available, otherwise fallback to "wwwroot"
		var rootPath = string.IsNullOrWhiteSpace(_env.WebRootPath)
			? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
			: _env.WebRootPath;

		var uploadsFolder = Path.Combine(rootPath, "uploads", subFolder);

		if (!Directory.Exists(uploadsFolder))
		{
			Directory.CreateDirectory(uploadsFolder);
		}

		var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
		var filePath = Path.Combine(uploadsFolder, uniqueFileName);

		using (var fileStream = new FileStream(filePath, FileMode.Create))
		{
			await file.CopyToAsync(fileStream);
		}

		return $"/uploads/{subFolder}/{uniqueFileName}";
	}
}