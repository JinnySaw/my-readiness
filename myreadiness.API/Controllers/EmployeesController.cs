using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myreadiness.API.Data;
using myreadiness.API.Dtos.EmployeeDtos;
using myreadiness.API.Models;
using myreadiness.API.Repositories;

namespace myreadiness.API.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase {
        private readonly IEmployeeRepository _repoEmp;
        private readonly IMapper _mapper;
        private readonly IRepository _repo;
        public EmployeesController (IRepository repo, IEmployeeRepository repoEmp, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
            _repoEmp = repoEmp;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees () {
            var employees = await _repoEmp.GetEmployees ();
            var employeeToReturn = _mapper.Map<IEnumerable<EmployeeForListDto>> (employees);
            return Ok (employeeToReturn);
        }

        [HttpGet ("employeesnotinusers")]
        public async Task<IActionResult> GetEmployeesNotInUsers () {
            var employees = await _repoEmp.GetEmployeesNotInUsers ();
            var employeeToReturn = _mapper.Map<IEnumerable<EmployeeForListDto>> (employees);
            return Ok (employeeToReturn);
        }

        [HttpGet ("{id}", Name = "GetEmployee")]
        public async Task<IActionResult> GetEmployee (Guid id) {
            var employee = await _repoEmp.GetEmployee (id);
            if (employee == null) {
                return NotFound();
            }
            var empToReturn = _mapper.Map<EmployeeForDetailDto> (employee);
            return Ok (empToReturn);
        }

        [HttpGet ("searchbyname/{name}")]
        public async Task<IActionResult> GetEmployeeByEmployeeName (string name) {
            var employee = await _repoEmp.GetEmployeeByName (name);
            if (employee == null) {
                return NotFound();
            }
            var empToReturn = _mapper.Map<EmployeeForDetailDto> (employee);
            return Ok (empToReturn);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateEmployee (EmployeeForCreationDto employeeForCreationDto) 
        {
            var empToCreate = _mapper.Map<Employee> (employeeForCreationDto);
            _repo.Add (empToCreate);
            
            if (await _repo.SaveAll()) {
                var empToReturn = _mapper.Map<EmployeeForDetailDto> (empToCreate);
                return CreatedAtRoute ("GetEmployee", new { id = empToCreate.Id }, empToReturn);
            }
            throw new Exception ("Creating the employee failed on save");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeForUpdateDto empUpdateDto)
        {
            if(id != empUpdateDto.Id)
            {
                return BadRequest();
            }
            var empFromRepo = await _repoEmp.GetEmployee(id);
            if(empFromRepo == null) {
                BadRequest("Could not find employee");
            }

            _mapper.Map(empUpdateDto, empFromRepo);

            if(await _repo.SaveAll()) {
                return NoContent();
            }
            throw new Exception($"Updating employee {id} failed on save");
        }
    }
}