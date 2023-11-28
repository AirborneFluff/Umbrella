using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Entities;

public abstract class MongoEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    public DateTime CreatedDate => Id.CreationTime;
}