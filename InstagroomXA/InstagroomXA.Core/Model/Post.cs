using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using SQLiteNetExtensions.Attributes;

namespace InstagroomXA.Core.Model
{
    /// <summary>
    /// User post data class
    /// </summary>
    public class Post
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }

        [MaxLength(32)]
        public string Username { get; set; }
        [MaxLength(255)]
        public string UserImagePath { get; set; }

        public int UserID { get; set; }
        public DateTime PostTime { get; set; }

        [MaxLength(255)]
        public string ImagePath { get; set; }
        [MaxLength(255)]
        public string ThumbnailPath { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public int Likes { get; set; }
        public int NumOfComments { get; set; }

        // [TextBlob("CommentsString")]
        // public List<string> Comments { get; set; }

        // [TextBlob("ImageString")]
        // public string Image { get; set; }

        // for serialization
        // public string CommentsString { get; set; }
        // public string ImageString { get; set; }
    }
}
