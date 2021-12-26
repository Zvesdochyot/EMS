using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace EMS.DAL.Models.Abstractions;

public abstract class EntityBase
{
    [BsonId(IdGenerator = typeof(CombGuidGenerator))]
    public virtual Guid Id { get; set; }

    [BsonRequired]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = BsonType.DateTime)]
    public virtual DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
