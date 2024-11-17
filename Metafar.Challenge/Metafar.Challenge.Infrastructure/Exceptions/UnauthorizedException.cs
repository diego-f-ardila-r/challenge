using System.Globalization;

namespace Metafar.Challenge.Infrastructure.Exceptions;

public class UnauthorizedException : SystemException
{
    public UnauthorizedException() : base() { }

    public UnauthorizedException(string message) : base(message) { }

    public UnauthorizedException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}