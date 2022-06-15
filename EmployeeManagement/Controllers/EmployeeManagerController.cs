using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.DTOs.Employee;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    //This for routing the commands
    [Route("[controller]")]
    public class EmployeeManagerController : ControllerBase
    {
        IEmployeeManagerService _employeeService;
        public EmployeeManagerController(IEmployeeManagerService EmployeeService)
        {
            _employeeService = EmployeeService;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<ServiceResponse<List<GetEmployeeDTO>>>> GetAllEmployeesAsync()
        {
            try
            {
                var resp = await _employeeService.GetAllEmployees();
                return Ok(resp);
            }
            catch (Exception e)
            {
                var resp = new ServiceResponse<string>();
                resp.Status = false;
                resp.Message = e.Message;
                return BadRequest(resp);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetEmployeeDTO>>> GetSingleEmployeeAsync(int id)
        {
            var resp = await _employeeService.GetEmployeeById(id);
            if (resp.Data == null)
            {
                //This will give status code 401
                return NotFound(resp);
            }
            else
            {
                return Ok(resp);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetEmployeeDTO>>>> AddEmployeeAsync([FromBody] AddEmployeeDTO newEmployee)
        {
            return Ok(await _employeeService.AddEmployee(newEmployee));

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetEmployeeDTO>>> UpdateEmployeeAsync(int id, [FromBody] UpdateEmployeeDTO updatedEmployee)
        {
            var resp = await _employeeService.UpdateEmployee(id, updatedEmployee);
            if (resp.Data == null)
            {
                //This will give status code 401
                return NotFound(resp);
            }
            else
            {
                return Ok(resp);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetEmployeeDTO>>>> DeleteEmployeeAsync(int id)
        {
            var resp = await _employeeService.DeleteEmployee(id);
            if (resp.Data == null)
            {
                //This will give status code 401
                return NotFound(resp);
            }
            else
            {
                return Ok(resp);
            }
        }
    }
}