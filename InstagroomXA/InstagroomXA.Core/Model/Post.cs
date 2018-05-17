using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace InstagroomXA.Core.Model
{
    /// <summary>
    /// User post data class
    /// </summary>
    public class Post
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }

        public int UserID { get; set; }
        public DateTime PostTime { get; set; }

        [MaxLength(255)]
        public string ImagePath { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public int Likes { get; set; }
    }
}
