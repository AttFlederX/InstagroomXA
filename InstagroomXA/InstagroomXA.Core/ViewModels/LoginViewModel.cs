using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmCross.Core.ViewModels;

using InstagroomXA.Core.Contracts;

namespace InstagroomXA.Core.ViewModels
{
    /// <summary>
    /// Log in screen view model
    /// </summary>
    public class LoginViewModel : MvxViewModel
    {
        private IUserDataService _userDataService;
        private IDialogService _dialogService;

        #region Commands 
        public IMvxCommand LoginCommand
        {
            get => new MvxCommand(() => 
            {

            });
        }

        public IMvxCommand LoginViaFacebookCommand
        {
            get => new MvxCommand(() =>
            {

            });
        }

        public IMvxCommand RegisterCommand
        {
            get => new MvxCommand(() =>
            {

            });
        }
        #endregion

        public LoginViewModel(IUserDataService userDataService, IDialogService dialogService)
        {
            _userDataService = userDataService;
            _dialogService = dialogService;
        }
    }
}
