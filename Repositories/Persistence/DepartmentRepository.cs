using Repositories.Core;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Persistence
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(TemplateDBContext templateDBContext) : base(templateDBContext)
        {

        }
        public async Task<IEnumerable<Department>> GetBestDeparments()
        {
            return await GetAll();
        }
    }
}
