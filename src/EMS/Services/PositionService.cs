using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMS.DAL.Contexts.Interfaces;
using EMS.DAL.Models;

namespace EMS.Services;

public class PositionService
{
    private readonly IMongoCollection<Position> _positions;

    public PositionService(IMongoContext context)
    {
        _positions = context.GetEntityCollection<Position>();
    }

    public async Task<List<Position>> GetAllAsync()
    {
        var queryResult = await _positions.FindAsync(position => true);
        return await queryResult.ToListAsync();
    }

    public async Task<Position?> GetByIdAsync(Guid id)
    {
        var queryResult = await _positions.FindAsync(position => position.Id == id); 
        return await queryResult.SingleOrDefaultAsync();
    }

    public Task CreateAsync(Position position)
    {
        return _positions.InsertOneAsync(position);
    }
}
