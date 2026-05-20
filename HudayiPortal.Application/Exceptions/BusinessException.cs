using System;

namespace HudayiPortal.Application.Exceptions;

/// <summary>
/// İş mantığı ve kontrollü iş kuralları hataları için fırlatılacak özel hata sınıfı.
/// </summary>
public class BusinessException : Exception
{
    public BusinessException(string message) : base(message)
    {
    }
}
