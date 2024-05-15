namespace GetAidBackend.Auth.Services.Implementations.Tests
{
    [TestClass()]
    public class PasswordHasherTests
    {
        [TestMethod()]
        public void HashTest()
        {
            var password = "string";
            var hasher = new PasswordHasher();
            var result = hasher.Hash(password);
            Assert.Fail();
        }
    }
}