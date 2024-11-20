using System.Globalization;

namespace Metafar.Challenge.Infrastructure.Exceptions;

public class NoContentException : SystemException
{
    public NoContentException() : base() { }

    public NoContentException(string message) : base(message) { }

    public NoContentException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}