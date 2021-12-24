using EMS.Configurations;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMS.DAL.Models;
using Microsoft.Extensions.Options;

namespace EMS.Services;

public class PositionService
{
    private readonly IMongoCollection<Position> _positions;

    public PositionService(IOptions<GeneralConfiguration> configuration)
    {
        var client = new MongoClient(configuration.Value.DatabaseConfiguration.ConnectionString);
        var database = client.GetDatabase(configuration.Value.DatabaseConfiguration.DatabaseName);
        _positions = database.GetCollection<Position>("Positions");
    }

    public async Task<List<Position>> GetAllAsync()
    {
        var queryResult = await _positions.FindAsync(position => true);
        return await queryResult.ToListAsync();
    }

    public async Task<Position?> GetByIdAsync(string id)
    {
        var queryResult = await _positions.FindAsync(position => position.Id == id); 
        return await queryResult.SingleOrDefaultAsync();
    }

    public Task CreateAsync(Position position)
    {
        return _positions.InsertOneAsync(position);
    }
}
