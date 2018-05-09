using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

using InstagroomXA.Core.Contracts;

namespace InstagroomXA.Core.ViewModels
{
    /// <summary>
    /// Log in screen view model
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        private IUserDataService _userDataService;
        private IDialogService _dialogService;

        private string _username;
        private string _password;
        private bool _isLoading;

        #region Bindable properties
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                RaisePropertyChanged(() => Username);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }
        #endregion

        #region Commands 
        public IMvxCommand LoginCommand
        {
            get => new MvxCommand(async () =>
            {
                if (string.IsNullOrWhiteSpace(Username))
                {
                    await _dialogService.ShowAlertAsync("Please enter your username", "Error", "OK");
                    return;
                }

                var user = await _userDataService.GetUserByUsernameAsync(Username);
                if (user != null)
                {
                    if (user.Password == Password)
                    {
                        _userDataService.CurrentUser = user;
                        // transfer to the master tabbed page
                        ShowViewModel<MasterTabControlViewModel>();
                    }
                    else
                    {
                        await _dialogService.ShowAlertAsync("Incorrect password. Try re-entering your credentials", "Error", "OK");
                        Password = string.Empty;
                    }
                }
                else
                {
                    await _dialogService.ShowAlertAsync($"User \"{Username}\" does not exist. Try re-entering your credentials", "Error",
                        "OK");
                    Password = string.Empty;
                }
            });
        }

        public IMvxCommand LoginViaFacebookCommand
        {
            get => new MvxCommand(() =>
            {
                // fetch the data from Facebook API
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

        public LoginViewModel(IMvxMessenger messenger, IUserDataService userDataService, IDialogService dialogService) : base(messenger)
        {
            _userDataService = userDataService;
            _dialogService = dialogService;
        }
    }
}
