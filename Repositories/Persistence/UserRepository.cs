using Repositories.Core;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Persistence
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TemplateDBContext context) : base(context)
        {
        }
    }
}
