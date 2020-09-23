using System;
using System.ComponentModel.DataAnnotations;

namespace myreadiness.API.Dtos.WebsiteDtos
{
    public class DomainForCreationDto
    {
         [Required]
        public string WebsiteAddress { get; set; }
        public bool? IsActive { get; set;}
        public bool? IsDeleted { get; set;}
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public DomainForCreationDto()
        {
            IsDeleted = false;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
    }
}