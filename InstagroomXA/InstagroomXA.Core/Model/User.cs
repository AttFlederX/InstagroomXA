using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace InstagroomXA.Core.Model
{
    /// <summary>
    /// User profile data class
    /// </summary>
    public class User
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }

        [MaxLength(255)]
        public string Username { get; set; }
        [MaxLength(255)]
        public string FirstName { get; set; }
        [MaxLength(255)]
        public string LastName { get; set; }
        [MaxLength(255)]
        public string Password { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
    }
}
