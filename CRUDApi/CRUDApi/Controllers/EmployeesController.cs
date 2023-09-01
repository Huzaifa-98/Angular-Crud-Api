using CRUDApi.Data;
using CRUDApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly CrudApiDbContext _crudApiDbContext;

        public EmployeesController(CrudApiDbContext crudApiDbContext)
        {
            _crudApiDbContext = crudApiDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employee = await _crudApiDbContext.Employees.ToListAsync();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();

            await _crudApiDbContext.Employees.AddAsync(employeeRequest);
            await _crudApiDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _crudApiDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployee)
        {
            var employee = await _crudApiDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            
            employee.Name = updateEmployee.Name;
            employee.Email = updateEmployee.Email;
            employee.Phone = updateEmployee.Phone;
            employee.Salary = updateEmployee.Salary;
            employee.Department = updateEmployee.Department;

            await _crudApiDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _crudApiDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _crudApiDbContext.Employees.Remove(employee);
            await _crudApiDbContext.SaveChangesAsync();

            return Ok(employee);
        }
    }
}
