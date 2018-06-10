using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstagroomXA.Core.Contracts;
using InstagroomXA.Core.Helpers;
using InstagroomXA.Core.Messages;
using InstagroomXA.Core.Model;

using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace InstagroomXA.Core.ViewModels
{
    /// <summary>
    /// View model for displaying other user's profile info
    /// </summary>
    public class UserProfileViewModel : BaseViewModel
    {
        private readonly IEnumService _enumService;
        private readonly IDialogService _dialogService;
        private readonly IUserDataService _userDataService;
        private readonly IPostDataService _postDataService;
        private readonly INotificationDataService _notificationDataService;

        private int _userId;

        private User _currentUser; // user whose profile info is displayed
        private MvxObservableCollection<Post> _postList;
        private int _isPostListEmpty;

        public User AppCurrentUser { get; set; } // user who's logged in at the moment
        public int UserId { get => _userId; }

        #region Bindable properties
        public User CurrentUser // user whose profile info is displayed
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
        public IMvxCommand FollowCommand
        {
            get => new MvxCommand(async () =>
            {
                if (!AppCurrentUser.Following.Contains(CurrentUser.ID))
                {
                    AppCurrentUser.Following.Add(CurrentUser.ID);
                    await _userDataService.UpdateUserAsync(AppCurrentUser);

                    CurrentUser.NumOfFollowers++;
                    await _userDataService.UpdateUserAsync(CurrentUser);

                    _dialogService.ShowPopupMessage($"You are now following {CurrentUser.Username}");

                    // send a notification to the followed user
                    var followNotif = new Notification()
                    {
                        PostID = -1, // no post associated w/ notif
                        SourceUserID = AppCurrentUser.ID,
                        TargetUserID = CurrentUser.ID,
                        Text = string.Format(ConstantHelper.FollowNotificationText, AppCurrentUser.Username),
                        Time = DateTime.Now,
                        SourceUserImagePath = AppCurrentUser.ImagePath
                    };
                    await _notificationDataService.AddNotificationAsync(followNotif);
                }
                else
                {
                    AppCurrentUser.Following.Remove(CurrentUser.ID);
                    await _userDataService.UpdateUserAsync(AppCurrentUser);

                    CurrentUser.NumOfFollowers--;
                    await _userDataService.UpdateUserAsync(CurrentUser);

                    _dialogService.ShowPopupMessage($"You've unfollowed {CurrentUser.Username}");
                }

                RaisePropertyChanged(() => CurrentUser); // update the count
                Messenger.Publish(new CurrentUserUpdatedMessage(this));
            });
        }

        public IMvxCommand PostSelectedCommand
        {
            get => new MvxCommand<Post>(selectedPost =>
            {
                ShowViewModel<PostViewModel>(new { postId = selectedPost.ID, userId = -1 });
            });
        }
        #endregion

        public UserProfileViewModel(IMvxMessenger messenger, IEnumService enumService, IDialogService dialogService,
            IUserDataService userDataService, IPostDataService postDataService,
            INotificationDataService notificationDataService) : base(messenger)
        {
            _enumService = enumService;
            _dialogService = dialogService;
            _userDataService = userDataService;
            _postDataService = postDataService;
            _notificationDataService = notificationDataService;

            AppCurrentUser = _userDataService.CurrentUser;
        }

        
        public void Init(int userId)
        {
            _userId = userId;
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
            CurrentUser = await _userDataService.GetUserByIDAsync(_userId);
            PostList = new MvxObservableCollection<Post>((await _postDataService.GetUserPosts(CurrentUser.ID,
                ConstantHelper.InitialPostsNum)).Reverse());
            IsPostListEmpty = (CurrentUser.NumOfPosts == 0) ? _enumService.ViewStateVisible : _enumService.ViewStateGone;
        }
    }
}
