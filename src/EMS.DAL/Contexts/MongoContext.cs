using System.Reflection;
using EMS.DAL.Attributes;
using EMS.DAL.Contexts.Interfaces;
using EMS.DAL.Models.Abstractions;
using MongoDB.Driver;

namespace EMS.DAL.Contexts;

public class MongoContext : IMongoContext
{
    private readonly IMongoDatabase _database;
    
    public MongoContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }
    
    public IMongoCollection<TEntity> GetEntityCollection<TEntity>() where TEntity : EntityBase
    {
        string collectionName = GetCollectionName(typeof(TEntity));
        return _database.GetCollection<TEntity>(collectionName);
    }
    
    private static string GetCollectionName(ICustomAttributeProvider entityInfo)
    {
        var attributes = entityInfo.GetCustomAttributes(typeof(CollectionNameAttribute), false);
        var attribute = (CollectionNameAttribute) attributes[0];
        return attribute.CollectionName;
    }
}
