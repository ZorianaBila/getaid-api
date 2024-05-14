using GetAidBackend.Services.Exceptions;

namespace GetAidBackend.Core.Services.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException()
            : base("Authorization required.") { }

        public UnauthorizedException(string message)
            : base(message) { }
    }
}