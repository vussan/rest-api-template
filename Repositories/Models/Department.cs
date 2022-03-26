using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public byte RowStateId { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
