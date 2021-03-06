using System;
using System.ComponentModel.DataAnnotations;

namespace myreadiness.API.Dtos.EmployeeDtos
{
    public class EmployeeForCreationDto
    {
        [Required]
        public string EmpName { get; set; }
        public DateTime? EmployDate {get;set;}
        public bool? Gender { get; set; }
        public bool? isPermanent { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }

        public EmployeeForCreationDto()
        {
            CreatedDate = DateTime.Now;
            LastModified = DateTime.Now;
        }
    }
}