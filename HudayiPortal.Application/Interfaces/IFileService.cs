using Microsoft.AspNetCore.Http;

namespace HudayiPortal.Application.Interfaces;

public interface IFileService
{
	Task<string> UploadFileAsync(IFormFile file, string subFolder);
}