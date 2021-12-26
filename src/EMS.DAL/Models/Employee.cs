using EMS.DAL.Attributes;
using EMS.DAL.Models.Abstractions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EMS.DAL.Models;

[CollectionName("Employees")]
public class Employee : EntityBase
{
    [BsonRequired]
    public string FirstName { get; set; }

    [BsonRequired]
    public string LastName { get; set; }

    [BsonRequired]
    public string Position { get; set; }

    [BsonRequired]
    public double Salary { get; set; }

    [BsonRequired]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = BsonType.DateTime)]
    public DateTime HiredOn { get; set; }

    [BsonDefaultValue(null)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = BsonType.DateTime)]
    public DateTime? FiredOn { get; set; }
}
