using HudayiPortal.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HudayiPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UploadController : ControllerBase
{
	private readonly IFileService _fileService;

	public UploadController(IFileService fileService)
	{
		_fileService = fileService;
	}

	[HttpPost("upload")]
	public async Task<IActionResult> UploadFile(IFormFile file, [FromForm] string folderName)
	{
		var fileUrl = await _fileService.UploadFileAsync(file, string.IsNullOrWhiteSpace(folderName) ? "genel" : folderName);

		return Ok(new { Url = fileUrl });
	}
}