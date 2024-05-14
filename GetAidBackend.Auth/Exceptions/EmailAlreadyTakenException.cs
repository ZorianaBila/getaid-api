using GetAidBackend.Domain;
using GetAidBackend.Services.Exceptions;

namespace GetAidBackend.Auth.Exceptions
{
    public class EmailAlreadyTakenException : BadInputException
    {
        public EmailAlreadyTakenException()
            : base("This email is already in use.") { }

        public EmailAlreadyTakenException(User person)
            : base($"This email ({person.Email}) is already in use.")
        {
        }
    }
}