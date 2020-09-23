using System;

namespace myreadiness.API.Dtos.WebsiteDtos
{
    public class DomainForUpdateDto
    {
        public Guid ID { get; set; }
        public string WebsiteAddress { get; set; }
        public bool? IsActive { get; set;}
        public bool? IsDeleted { get; set;}
        public DateTime? UpdatedDate { get; set; }

        public DomainForUpdateDto()
        { 
            UpdatedDate = DateTime.Now;
        }
    }
}