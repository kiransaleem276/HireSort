using System;
using System.Collections.Generic;

namespace HireSort.Entity.DbModels
{
    public partial class ClientHighlight
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Category { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual Client Client { get; set; } = null!;
    }
}
