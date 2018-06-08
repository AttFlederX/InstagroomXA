using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmCross.Plugins.Messenger;

namespace InstagroomXA.Core.Messages
{
    /// <summary>
    /// Message class for current user change handling
    /// </summary>
    public class CurrentUserUpdatedMessage : MvxMessage
    {
        public CurrentUserUpdatedMessage(object sender) : base(sender)
        {
        }
    }
}
