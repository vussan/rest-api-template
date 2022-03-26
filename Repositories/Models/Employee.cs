using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid? DepartmentId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public byte RowStateId { get; set; }

        public virtual Department? Department { get; set; }
    }
}
