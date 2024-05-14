using MongoDB.Bson.Serialization.Attributes;

namespace GetAidBackend.Domain
{
    public class User : EntityBase
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public UserRole UserRole { get; set; }

        public UserPrivateData PrivateData { get; set; }
    }
}