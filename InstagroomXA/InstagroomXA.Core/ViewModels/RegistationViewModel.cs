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
    /// Registration screen view model
    /// </summary>
    public class RegistationViewModel : MvxViewModel
    {
        private IUserDataService _userDataService;
        private IDialogService _dialogService;

        #region Commands 
        public IMvxCommand SignUpCommand
        {
            get => new MvxCommand(() =>
            {

            });
        }
        #endregion

        public RegistationViewModel(IUserDataService userDataService, IDialogService dialogService)
        {
            _userDataService = userDataService;
            _dialogService = dialogService;
        }
    }
}
