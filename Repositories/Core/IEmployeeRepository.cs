using Repositories.Models;
using Repositories.Models.SPModels;

namespace Repositories.Core
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetTopEmployees();
        Task<IEnumerable<GetEmployeeByUser>> GetByUser(Guid userId, DateTime startDate, DateTime endDate);
    }
}