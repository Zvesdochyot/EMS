using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EMS.DAL.Models;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Position { get; set; }

    public long Salary { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = BsonType.DateTime)]
    public DateTime Hired { get; set; }

    [BsonDefaultValue(null)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = BsonType.DateTime)]
    public DateTime? Fired { get; set; }
}
