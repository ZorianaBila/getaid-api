using GetAidBackend.Core.Services.Exceptions;

namespace GetAidBackend.Auth.Exceptions
{
    public class PasswordMismatchException : UnauthorizedException
    {
        public PasswordMismatchException()
            : base("Password mismatch") { }
    }
}