﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

using InstagroomXA.Core.Contracts;
using InstagroomXA.Core.Model;

namespace InstagroomXA.Core.ViewModels
{
    /// <summary>
    /// Registration screen view model
    /// </summary>
    public class RegistationViewModel : BaseViewModel
    {
        private readonly IUserDataService _userDataService;
        private readonly IDialogService _dialogService;
        private readonly IValidationService _validationService;

        private string _username;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        private string _repeatPassword;

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

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                RaisePropertyChanged(() => LastName);
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
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

        public string RepeatPassword
        {
            get => _repeatPassword;
            set
            {
                _repeatPassword = value;
                RaisePropertyChanged(() => RepeatPassword);
            }
        }
        #endregion

        #region Commands 
        public IMvxCommand SignUpCommand
        {
            get => new MvxCommand(async () =>
            {
                if (string.IsNullOrWhiteSpace(Username))
                {
                    await _dialogService.ShowAlertAsync("Please enter your username", "Error", "OK");
                    return;
                }
                // query user by username (which should be unique)
                var userProfile = await _userDataService.GetUserByUsernameAsync(Username.ToLowerInvariant()); 

                if (userProfile != null) // user already exists
                {
                    await _dialogService.ShowAlertAsync("This username is already taken. Please pick another one", "Error", "OK");
                    Password = string.Empty;
                    return;
                }

                if (string.IsNullOrWhiteSpace(FirstName))
                {
                    await _dialogService.ShowAlertAsync("Please enter your first name", "Error", "OK");
                    return;
                }
                else if(string.IsNullOrWhiteSpace(LastName))
                {
                    await _dialogService.ShowAlertAsync("Please enter your last name", "Error", "OK");
                    return;
                }
                else if(!_validationService.IsEmailValid(Email))
                {
                    await _dialogService.ShowAlertAsync("Please enter a valid email address", "Error", "OK");
                }
                else if (!_validationService.IsPasswordValid(Password))
                {
                    await _dialogService.ShowAlertAsync($"Please enter a valid password ({_validationService.PasswordCriteria})", "Error",
                        "OK");
                    Password = string.Empty;
                    RepeatPassword = string.Empty;
                }
                else if (Password != RepeatPassword)
                {
                    await _dialogService.ShowAlertAsync("The passwords don't match", "Error", "OK");
                    Password = string.Empty;
                    RepeatPassword = string.Empty;
                }
                else
                {
                    var newUserProfile = new User()
                    {
                        Username = this.Username.ToLowerInvariant(),
                        FirstName = this.FirstName,
                        LastName = this.LastName,
                        Email = this.Email,
                        Password = this.Password,
                        Followers = new List<int>(),
                        Following = new List<int>(),
                        NumOfPosts = 0,
                        FollowersString = string.Empty,
                        FollowingString = string.Empty
                    };

                    await _userDataService.AddUserAsync(newUserProfile);
                    // close the page after the user presses OK
                    await _dialogService.ShowAlertAsync("Your account has been registered", "Success", "OK", () => Close(this));
                }
            });
        }

        public IMvxCommand LoginCommand
        {
            get => new MvxCommand(() =>
            {
                ShowViewModel<LoginViewModel>();
            });
        }
        #endregion

        public RegistationViewModel(IMvxMessenger messenger, IUserDataService userDataService, IDialogService dialogService, 
            IValidationService validationService) : base(messenger)
        {
            _userDataService = userDataService;
            _dialogService = dialogService;
            _validationService = validationService;
        }
    }
}
