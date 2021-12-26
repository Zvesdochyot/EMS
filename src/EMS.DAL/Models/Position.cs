using EMS.DAL.Attributes;
using EMS.DAL.Models.Abstractions;
using MongoDB.Bson.Serialization.Attributes;

namespace EMS.DAL.Models;

[CollectionName("Positions")]
public class Position : EntityBase
{
    [BsonRequired]
    public string Title { get; set; }
    
    public string Description { get; set; }
}
