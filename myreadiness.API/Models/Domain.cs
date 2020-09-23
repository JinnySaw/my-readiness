using System;
namespace myreadiness.API.Models
{
    public class Domain
    {
        public Guid ID { get; set; }
        public string DomainAddress { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
