using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMS.DAL.Contexts.Interfaces;
using EMS.DAL.Models;

namespace EMS.Services;

public class EmployeeService
{
    private readonly IMongoCollection<Employee> _employees;

    public EmployeeService(IMongoContext context)
    {
        _employees = context.GetEntityCollection<Employee>();
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        var queryResult = await _employees.FindAsync(_ => true);
        return await queryResult.ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        var queryResult = await _employees.FindAsync(employee => employee.Id == id);
        return await queryResult.SingleOrDefaultAsync();
    }

    public Task CreateAsync(Employee employee)
    {
        return _employees.InsertOneAsync(employee);
    }

    public Task UpdateAsync(Guid id, Employee updatedEmployee)
    {
        return _employees.ReplaceOneAsync(employee => employee.Id == id, updatedEmployee);
    }
    
    public Task RemoveByIdAsync(Guid id)
    {
        return _employees.DeleteOneAsync(employee => employee.Id == id);
    }
    
    public Task RemoveAsync(Employee employeeToRemove)
    {
        return _employees.DeleteOneAsync(employee => employee.Id == employeeToRemove.Id);
    }
}
