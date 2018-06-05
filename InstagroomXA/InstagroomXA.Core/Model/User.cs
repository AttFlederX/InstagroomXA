using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensionsAsync;

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

        [TextBlob("FollowersString")]
        public List<int> Followers { get; set; }
        [TextBlob("FollowingString")]
        public List<int> Following { get; set; }
        [TextBlob("LikedPostsString")]
        public List<int> LikedPosts { get; set; }

        public int NumOfPosts { get; set; }

        public string FullName { get => $"{FirstName} {LastName}"; }

        // for serialization
        public string FollowersString { get; set; }
        public string FollowingString { get; set; }
        public string LikedPostsString { get; set; }
    }
}
