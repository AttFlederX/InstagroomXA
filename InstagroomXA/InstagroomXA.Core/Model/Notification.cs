using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace InstagroomXA.Core.Model
{
    /// <summary>
    /// User notification data class
    /// </summary>
    public class Notification
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }

        public int TargetUserID { get; set; }
        public int SourceUserID { get; set; }
        public int PostID { get; set; }
        public DateTime Time { get; set; }

        [MaxLength(255)]
        public string Text { get; set; }
        [MaxLength(255)]
        public string SourceUserImagePath { get; set; }
    }
}
