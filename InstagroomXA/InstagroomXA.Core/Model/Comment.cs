using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace InstagroomXA.Core.Model
{
    /// <summary>
    /// Post comment data class
    /// </summary>
    public class Comment
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }

        public int CommenterID { get; set; }
        public int PostID { get; set; }
        public DateTime PostTime { get; set; }

        [MaxLength(255)]
        public string CommenterUsername { get; set; }
        [MaxLength(255)]
        public string Text { get; set; }
    }
}
