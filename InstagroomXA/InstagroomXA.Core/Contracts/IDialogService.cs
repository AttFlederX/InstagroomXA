using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagroomXA.Core.Contracts
{
    /// <summary>
    /// Interface for message dialog services' classes
    /// </summary>
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonText, Action okCallback = null);
        void ShowPopupMessage(string message);
    }
}
