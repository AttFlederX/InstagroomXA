using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagroomXA.Core.Model
{
    /// <summary>
    /// User post data class
    /// </summary>
    public class Post
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public DateTime PostTime { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; }
    }
}
