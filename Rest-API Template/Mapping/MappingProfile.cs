using AutoMapper;
using Repositories.DTO;
using Repositories.Models;

namespace Rest_API_Template
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, DDLDTO>();
            CreateMap<Employee, EmployeeDTO>();
        }
    }
}
