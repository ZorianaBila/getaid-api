namespace GetAidBackend.Auth.Services.Abstractions
{
    public interface IPasswordHasher
    {
        string Hash(string password);

        bool Verify(string password, string hashedPassword);
    }
}