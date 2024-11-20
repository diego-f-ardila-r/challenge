using System.Globalization;

namespace Metafar.Challenge.Infrastructure.Exceptions;

public class ValidatorException : SystemException
{
    public ValidatorException() : base() { }

    public ValidatorException(string message) : base(message) { }

    public ValidatorException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
    
    public ValidatorException(string message, IEnumerable<dynamic> data)
        : base(message)
    {
        Data.Add("Errors", data);
    }
}