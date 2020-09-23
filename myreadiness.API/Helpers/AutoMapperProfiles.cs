using AutoMapper;
using myreadiness.API.Dtos.UserDtos;
using myreadiness.API.Models;
using myreadiness.API.Dtos.WebsiteDtos;
using myreadiness.API.Dtos.EmployeeDtos;

namespace myreadiness.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>();
            CreateMap<User, UserForDetailDto>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserForUpdateDto, User>();

            CreateMap<DomainForCreationDto, Domain>();
            CreateMap<DomainForUpdateDto, Domain>();
            CreateMap<Domain, DomainForListDto>();
            CreateMap<Domain, DomainForDetailDto>();

            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<EmployeeForUploadDto, Employee>();
            CreateMap<Employee, EmployeeForDetailDto>();
            CreateMap<EmployeeForUpdateDto, Employee>();
        }
    }
}