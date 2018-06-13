using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstagroomXA.Core.Contracts;

using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace InstagroomXA.Core.ViewModels
{
    /// <summary>
    /// Welcome screen view model
    /// Includes two commands for 'Log in' & 'Register' buttons
    /// </summary>
    public class WelcomeViewModel : BaseViewModel
    {
        private readonly IUserDataService _userDataService;

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
        public IMvxCommand AutoLoginCommand
        {
            get => new MvxCommand<int>(async userId =>
            {
                _userDataService.CurrentUser = await _userDataService.GetUserByIDAsync(userId);
                ShowViewModel<MasterTabControlViewModel>(presentationBundle:
                            new MvxBundle(new Dictionary<string, string> { { "ClearStack", "" } }));
            });
        }
        #endregion

        public WelcomeViewModel(IMvxMessenger messenger, IUserDataService userDataService) : base(messenger)
        {
            _userDataService = userDataService;
        }
    }
}
