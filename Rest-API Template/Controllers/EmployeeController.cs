using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.DTO;
using Repositories.UnitOfWork;

namespace Rest_API_Template.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("ddl")]
        public async Task<IEnumerable<DDLDTO>> GetDDL()
        {
            var employees = await _unitOfWork.Employees.GetAll();
            return _mapper.Map<IEnumerable<DDLDTO>>(employees);
        }
    }
}