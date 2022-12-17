using System;
using System.Collections.Generic;

namespace HireSort.Entity.DbModels
{
    public partial class ContactU
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; } = null!;

        public virtual Client Client { get; set; } = null!;
    }
}
