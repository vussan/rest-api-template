using Repositories.Core;
using Repositories.Models;
using Repositories.Persistence;

namespace Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TemplateDBContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        public UnitOfWork(TemplateDBContext context)
        {
            _context = context;
        }
        public void SaveAsync() => _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();

        public IUserRepository Users { get => _userRepository ?? new UserRepository(_context); }
        public IEmployeeRepository Employees { get => _employeeRepository ?? new EmployeeRepository(_context); }
        public IDepartmentRepository Departments { get => _departmentRepository ?? new DepartmentRepository(_context); }

        
    }
}
