using Repositories.Models;

namespace Repositories.Core
{
    public  interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetBestDeparments();
    }
}
