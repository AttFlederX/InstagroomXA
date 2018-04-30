using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmCross.Core.ViewModels;

namespace InstagroomXA.Core.ViewModels
{
    /// <summary>
    /// Welcome screen view model
    /// Includes two commands for 'Log in' & 'Register' buttons
    /// </summary>
    public class WelcomeViewModel : MvxViewModel
    {
        #region Commands
        public IMvxCommand LoginCommand
        {
            get => new MvxCommand(() =>
            {
                ShowViewModel<LoginViewModel>();
            });
        }

        public IMvxCommand RegisterCommand
        {
            get => new MvxCommand(() =>
            {
                ShowViewModel<RegistationViewModel>();
            });
        }
        #endregion

        public WelcomeViewModel() { }
    }
}
