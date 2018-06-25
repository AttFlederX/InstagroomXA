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
    /// View model for user's feed tab
    /// Used to display the posts from people followed by the user
    /// </summary>
    public class FeedViewModel : BaseViewModel
    {
        private readonly IEnumService _enumService;
        private readonly IUserDataService _userDataService;
        private readonly IPostDataService _postDataService;

        private User _currentUser;
        private MvxObservableCollection<Post> _postList;
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
        public IMvxCommand PostSelectedCommand
        {
            get => new MvxCommand<Post>(selectedPost =>
            {
                ShowViewModel<PostViewModel>(new { postId = selectedPost.ID });
            });
        }
        #endregion

        public FeedViewModel(IMvxMessenger messenger, IEnumService enumService, IUserDataService userDataService, 
            IPostDataService postDataService) : base(messenger)
        {
            _enumService = enumService;
            _userDataService = userDataService;
            _postDataService = postDataService;

            CurrentUser = _userDataService.CurrentUser;
            Messenger.Subscribe<PostUpdatedMessage>(async message => await InitializeAsync(), MvxReference.Strong);
        }


        public override async void Start()
        {
            base.Start();
            await ReloadDataAsync();
        }

        /// <summary>
        /// Method for asynchronous post data fetching
        /// </summary>
        /// <returns></returns>
        protected override async Task InitializeAsync()
        {
            var pList = new MvxObservableCollection<Post>();
            foreach (int userId in CurrentUser.Following)
            {
                pList.AddRange(await _postDataService.GetUserPosts(userId, ConstantHelper.InitialPostsNum));
            }
            PostList = new MvxObservableCollection<Post>(pList.OrderByDescending(p => p.PostTime)); // sort by date

            IsPostListEmpty = (PostList.Count == 0) ? _enumService.ViewStateVisible : _enumService.ViewStateGone;
        }
    }
}
