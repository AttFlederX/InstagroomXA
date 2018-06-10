using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagroomXA.Core.Messages
{
    /// <summary>
    /// Message class for post data change handling
    /// </summary>
    public class PostUpdatedMessage : MvxMessage
    {
        public int PostID { get; set; }

        public PostUpdatedMessage(object sender) : base(sender)
        {

        }
    }
}
