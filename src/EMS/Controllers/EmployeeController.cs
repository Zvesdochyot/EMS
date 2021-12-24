using EMS.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMS.DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace EMS.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
    {
        await _employeeService.CreateAsync(employee);
        return CreatedAtRoute("GetEmployee", new { id = employee.Id }, employee);
    }

    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetEmployees() => await _employeeService.GetAllAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Employee>> GetEmployee(string id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        return employee != null ? employee : NotFound();
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateEmployee(string id, Employee updated)
    {
        var employee = await _employeeService.GetByIdAsync(id);

        if (employee == null)
        {
            return NotFound();
        }

        await _employeeService.UpdateAsync(id, updated);
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        var employee = await _employeeService.GetByIdAsync(id);

        if (employee == null)
        {
            return NotFound();
        }

        await _employeeService.RemoveByIdAsync(employee.Id);
        return NoContent();
    }
}
