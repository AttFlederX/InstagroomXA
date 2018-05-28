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
    /// View model for user's profile information tab
    /// </summary>
    public class ProfileViewModel : BaseViewModel
    {
        private readonly IUserDataService _userDataService;
        private readonly IPostDataService _postDataService;

        private User _currentUser;
        private MvxObservableCollection<Post> _postList;
        // private Post _selectedPost;
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

        public MvxObservableCollection<Post> PostList
        {
            get => _postList;
            set
            {
                _postList = value;
                RaisePropertyChanged(() => PostList);
            }
        }

        //public Post SelectedPost
        //{
        //    get => _selectedPost;
        //    set
        //    {
        //        _selectedPost = value;
        //        RaisePropertyChanged(() => SelectedPost);
        //    }
        //}

        public int IsPostListEmpty // for textview visibility
        {
            get => _isPostListEmpty;
            set
            {
                _isPostListEmpty = value;
                RaisePropertyChanged(() => IsPostListEmpty);
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

        public IMvxCommand PostSelectedCommand
        {
            get => new MvxCommand(async () =>
            {
                ShowViewModel<LoginViewModel>();
            });
        }
        #endregion

        public ProfileViewModel(IMvxMessenger messenger, IUserDataService userDataService, 
            IPostDataService postDataService) : base(messenger)
        {
            _userDataService = userDataService;
            _postDataService = postDataService;

            CurrentUser = _userDataService.CurrentUser;
        }


        public override async void Start()
        {
            base.Start();
            await ReloadDataAsync();
        }

        /// <summary>
        /// Method for asynchronous post data fetch
        /// </summary>
        /// <returns></returns>
        protected override async Task InitializeAsync()
        {
            PostList = new MvxObservableCollection<Post>((await _postDataService.GetUserPosts(_userDataService.CurrentUser.ID, 
                ConstantHelper.InitialPostsNum)).Reverse());
            IsPostListEmpty = (CurrentUser.NumOfPosts == 0) ? 0 : 8; // int values taken from ViewStates enum
        }
    }
}
