using Repositories.Core;

namespace Repositories.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        void SaveAsync();
        IUserRepository Users { get; }
        IEmployeeRepository Employees { get; }
        IDepartmentRepository Departments { get; }
    }
}
