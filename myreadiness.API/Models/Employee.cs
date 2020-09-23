using System;

namespace myreadiness.API.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string EmpName { get; set; }
        public DateTime? EmployDate { get; set; }
        public bool? Gender {get; set;}
        public bool? isPermanent { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModified { get; set; }

    }
}