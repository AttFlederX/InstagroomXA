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
    /// View model for user's notifications tab
    /// Displays user's notifications like new followers or liked posts
    /// </summary>
    public class NotificationsViewModel : BaseViewModel
    {
        private readonly IEnumService _enumService;
        private readonly IUserDataService _userDataService;
        private readonly INotificationDataService _notificationDataService;

        private User _currentUser;
        private MvxObservableCollection<Notification> _notificationList;
        private int _isNotificationListEmpty;

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

        public MvxObservableCollection<Notification> NotificationList
        {
            get => _notificationList;
            set
            {
                _notificationList = value;
                RaisePropertyChanged(() => NotificationList);
            }
        }

        public int IsNotificationListEmpty // for textview visibility
        {
            get => _isNotificationListEmpty;
            set
            {
                _isNotificationListEmpty = value;
                RaisePropertyChanged(() => IsNotificationListEmpty);
            }
        }
        #endregion

        #region Commands 
        public IMvxCommand NotificationSelectedCommand
        {
            get => new MvxCommand<Notification>(selectedNotification =>
            {
                if (selectedNotification.PostID < 0) // if no post was associated w/ notif
                {
                    ShowViewModel<UserProfileViewModel>(new { userId = selectedNotification.SourceUserID });
                }
                else
                {
                    ShowViewModel<PostViewModel>(new { postId = selectedNotification.PostID, userId = selectedNotification.SourceUserID });
                }
            });
        }
        #endregion

        public NotificationsViewModel(IMvxMessenger messenger, IEnumService enumService, IUserDataService userDataService,
            INotificationDataService notificationDataService) : base(messenger)
        {
            _enumService = enumService;
            _userDataService = userDataService;
            _notificationDataService = notificationDataService;

            CurrentUser = _userDataService.CurrentUser;
            // Messenger.Subscribe<PostUpdatedMessage>(message => RaisePropertyChanged(() => PostList), MvxReference.Strong);
        }


        public override async void Start()
        {
            base.Start();
            await ReloadDataAsync();
        }

        /// <summary>
        /// Method for asynchronous notification data fetching
        /// </summary>
        /// <returns></returns>
        protected override async Task InitializeAsync()
        {
            NotificationList = new MvxObservableCollection<Notification>((
                await _notificationDataService.GetUserNotifications(CurrentUser.ID)).Reverse()); // sort by date

            IsNotificationListEmpty = (NotificationList.Count == 0) ? _enumService.ViewStateVisible : _enumService.ViewStateGone;
        }
    }
}
