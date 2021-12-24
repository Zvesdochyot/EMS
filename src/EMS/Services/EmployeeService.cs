using EMS.Configurations;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMS.DAL.Models;
using Microsoft.Extensions.Options;

namespace EMS.Services;

public class EmployeeService
{
    private readonly IMongoCollection<Employee> _employees;

    public EmployeeService(IOptions<GeneralConfiguration> configuration)
    {
        var client = new MongoClient(configuration.Value.DatabaseConfiguration.ConnectionString);
        var database = client.GetDatabase(configuration.Value.DatabaseConfiguration.DatabaseName);
        _employees = database.GetCollection<Employee>("Employees");
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        var queryResult = await _employees.FindAsync(_ => true);
        return await queryResult.ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(string id)
    {
        var queryResult = await _employees.FindAsync(employee => employee.Id == id);
        return await queryResult.SingleOrDefaultAsync();
    }

    public Task CreateAsync(Employee employee)
    {
        return _employees.InsertOneAsync(employee);
    }

    public Task UpdateAsync(string id, Employee updatedEmployee)
    {
        return _employees.ReplaceOneAsync(employee => employee.Id == id, updatedEmployee);
    }
    
    public Task RemoveByIdAsync(string id)
    {
        return _employees.DeleteOneAsync(employee => employee.Id == id);
    }
    
    public Task RemoveAsync(Employee employeeToRemove)
    {
        return _employees.DeleteOneAsync(employee => employee.Id == employeeToRemove.Id);
    }
}
