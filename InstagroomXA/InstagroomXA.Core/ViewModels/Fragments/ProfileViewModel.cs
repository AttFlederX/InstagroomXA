using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstagroomXA.Core.Contracts;
using InstagroomXA.Core.Model;

using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace InstagroomXA.Core.ViewModels
{
    /// <summary>
    /// View model for user's profile information tab
    /// </summary>
    public class ProfileViewModel : BaseViewModel
    {
        private readonly IUserDataService _userDataService;
        private readonly IPostDataService _postDataService;

        private User _currentUser;
        private int _isPostListEmpty;

        #region Bindable properties
        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                RaisePropertyChanged(() => CurrentUser);
            }
        }

        public int IsPostListEmpty // for textview visibility
        {
            get => _isPostListEmpty;
            set
            {
                _isPostListEmpty = value;
                RaisePropertyChanged(() => CurrentUser);
            }
        }
        #endregion

        #region Commands 
        public IMvxCommand EditProfileCommand
        {
            get => new MvxCommand(async () =>
            {
                // ShowViewModel<LoginViewModel>();
            });
        }
        #endregion

        public ProfileViewModel(IMvxMessenger messenger, IUserDataService userDataService, 
            IPostDataService postDataService) : base(messenger)
        {
            _userDataService = userDataService;
            _postDataService = postDataService;

            CurrentUser = _userDataService.CurrentUser;
            IsPostListEmpty = (CurrentUser.NumOfPosts == 0) ? 0 : 8; // int values taken from ViewStates enum
        }
    }
}
