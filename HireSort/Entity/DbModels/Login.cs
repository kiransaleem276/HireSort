using System;
using System.Collections.Generic;

namespace HireSort.Entities.DbModels
{
    public partial class Login
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsActive { get; set; }
        public string? CreatedOn { get; set; }

        public virtual Client Client { get; set; } = null!;
    }
}
