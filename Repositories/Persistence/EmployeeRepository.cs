using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repositories.Core;
using Repositories.Models;
using Repositories.Models.SPModels;

namespace Repositories.Persistence
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly TemplateDBContext _dbContext;
        public EmployeeRepository(TemplateDBContext templateDBContext) : base(templateDBContext)
        {
            _dbContext = templateDBContext;
        }
        public async Task<IEnumerable<Employee>> GetTopEmployees()
        {
            return await _dbContext.Employees.Take(100).ToListAsync();
        }

        public async Task<IEnumerable<GetEmployeeByUser>> GetByUser(Guid userId, DateTime startDate, DateTime endDate)
        {
            var userIdParam = new SqlParameter("UserId", userId);
            var startDateParam = new SqlParameter("StartDate", startDate);
            var endDateParam = new SqlParameter("EndDate", endDate);
            return await _dbContext.EmployeeByUserSP.FromSqlInterpolated($"[usp_GetEmployeeByUser] {userIdParam},{startDateParam},{endDateParam}").ToListAsync();
        }
    }
}
