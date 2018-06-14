using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstagroomXA.Core.Contracts;
using InstagroomXA.Core.Helpers;
using InstagroomXA.Core.Model;

using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace InstagroomXA.Core.ViewModels
{
    /// <summary>
    /// Log in vai Facebook screen view model
    /// </summary>
    public class FacebookLoginViewModel : BaseViewModel
    {
        private readonly IEnumService _enumService;
        private readonly IUserDataService _userDataService;
        private readonly IDialogService _dialogService;

        private string _username;
        private string _fullName;
        private string _imagePath;
        private User _newUser;
        private int _isUserRegistered;

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

        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                RaisePropertyChanged(() => FullName);
            }
        }

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                RaisePropertyChanged(() => ImagePath);
            }
        }

        public User NewUser
        {
            get => _newUser;
            set
            {
                _newUser = value;
                RaisePropertyChanged(() => NewUser);
            }
        }

        public int IsUserRegistered
        {
            get => _isUserRegistered;
            set
            {
                _isUserRegistered = value;
                RaisePropertyChanged(() => IsUserRegistered);
            }
        }
        #endregion

        #region Commands 
        public IMvxCommand LoginViaFacebookCommand
        {
            get => new MvxCommand(async () =>
            {
                var usr = await _userDataService.GetUserByFacebookIDAsync(NewUser.FacebookID);
                if (usr != null) // log the user in
                {
                    _userDataService.CurrentUser = usr;

                    ShowViewModel<MasterTabControlViewModel>(presentationBundle:
                            new MvxBundle(new Dictionary<string, string> { { "ClearStack", "" } }));
                }
                else // username required to complete registration
                {
                    IsUserRegistered = _enumService.ViewStateVisible;
                    _dialogService.ShowPopupMessage("Please enter the username for your new account\nIt will be used to identify you on Instagroom");
                }
            });
        }

        public IMvxCommand RegisterCommand
        {
            get => new MvxCommand(async () =>
            {
                if (string.IsNullOrWhiteSpace(Username))
                {
                    _dialogService.ShowPopupMessage("Please enter your username");
                    return;
                }
                // query user by username (which should be unique)
                var userProfile = await _userDataService.GetUserByUsernameAsync(Username.ToLowerInvariant());

                if (userProfile != null) // user already exists
                {
                    await _dialogService.ShowAlertAsync("This username is already taken. Please pick another one", "Error", "OK");
                    return;
                }
                else
                {
                    var newUserProfile = new User()
                    {
                        Username = this.Username.ToLowerInvariant(),
                        FacebookID = NewUser.FacebookID,
                        ImagePath = NewUser.ImagePath,
                        FirstName = NewUser.FirstName,
                        LastName = NewUser.LastName,
                        Email = string.Empty,
                        Password = string.Empty,
                        Following = new List<int>(),
                        LikedPosts = new List<int>(),
                        NumOfFollowers = 0,
                        NumOfPosts = 0,
                        FollowingString = string.Empty,
                        LikedPostsString = string.Empty
                    };

                    await _userDataService.AddUserAsync(newUserProfile);
                    _dialogService.ShowPopupMessage("Your account has been registered");

                    _userDataService.CurrentUser = newUserProfile;
                    ShowViewModel<MasterTabControlViewModel>(presentationBundle:
                            new MvxBundle(new Dictionary<string, string> { { "ClearStack", "" } }));
                }
            });
        }
        #endregion

        public FacebookLoginViewModel(IMvxMessenger messenger, IEnumService enumService, IUserDataService userDataService, 
            IDialogService dialogService) : base(messenger)
        {
            _enumService = enumService;
            _userDataService = userDataService;
            _dialogService = dialogService;

            NewUser = new User();
            IsUserRegistered = _enumService.ViewStateGone; // hide the username entry
        }
    }
}
