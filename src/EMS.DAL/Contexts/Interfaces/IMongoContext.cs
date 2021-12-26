using EMS.DAL.Models.Abstractions;
using MongoDB.Driver;

namespace EMS.DAL.Contexts.Interfaces;

public interface IMongoContext
{
    public IMongoCollection<TEntity> GetEntityCollection<TEntity>() where TEntity : EntityBase;
}
