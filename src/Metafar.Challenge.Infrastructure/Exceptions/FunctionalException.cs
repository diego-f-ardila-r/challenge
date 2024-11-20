using System.Globalization;

namespace Metafar.Challenge.Infrastructure.Exceptions;

public class FunctionalException : SystemException
{
    public FunctionalException() : base() { }

    public FunctionalException(string message) : base(message) { }

    public FunctionalException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}