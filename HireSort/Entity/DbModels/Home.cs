using System;
using System.Collections.Generic;

namespace HireSort.Entities.DbModels
{
    public partial class Home
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Type { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual Client Client { get; set; } = null!;
    }
}
