namespace HudayiPortal.Application.Wrappers;

public sealed class ErrorResult
{
	public bool Succeeded { get; set; } = false;
	public string Message { get; set; } = string.Empty;
	public List<string> Errors { get; set; } = new();
}