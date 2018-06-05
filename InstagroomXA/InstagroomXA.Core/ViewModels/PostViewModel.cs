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
    /// View model for post details (description, comments, likes)
    /// </summary>
    public class PostViewModel : BaseViewModel
    {
        private readonly IUserDataService _userDataService;
        private readonly IPostDataService _postDataService;

        private int _postId;
        private int _userId;

        private User _currentUser;
        private MvxObservableCollection<Post> _commentList;
        private Post _currentPost;
        private int _isCommentListEmpty;

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

        public MvxObservableCollection<Post> CommentList
        {
            get => _commentList;
            set
            {
                _commentList = value;
                RaisePropertyChanged(() => CommentList);
            }
        }

        public Post CurrentPost
        {
            get => _currentPost;
            set
            {
                _currentPost = value;
                RaisePropertyChanged(() => CurrentPost);
            }
        }

        public int IsCommentListEmpty // for textview visibility
        {
            get => _isCommentListEmpty;
            set
            {
                _isCommentListEmpty = value;
                RaisePropertyChanged(() => IsCommentListEmpty);
            }
        }
        #endregion

        #region Commands 
        public IMvxCommand LikeCommand
        {
            get => new MvxCommand(async () =>
            {
                ShowViewModel<LoginViewModel>();
            });
        }

        public IMvxCommand AddCommentCommand
        {
            get => new MvxCommand(async () =>
            {
                ShowViewModel<LoginViewModel>();
            });
        }
        #endregion

        public PostViewModel(IMvxMessenger messenger, IUserDataService userDataService,
            IPostDataService postDataService) : base(messenger)
        {
            _userDataService = userDataService;
            _postDataService = postDataService;

            CurrentUser = _userDataService.CurrentUser;
        }


        public void Init(int postId, int userId)
        {
            _postId = postId;
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
            CurrentUser = (_userId == -1) ? _userDataService.CurrentUser : await _userDataService.GetUserByIDAsync(_userId);
            CurrentPost = await _postDataService.GetPostByIDAsync(_postId);
            // CommentList = new MvxObservableCollection<Post>((await _postDataService.GetUserPosts(_userDataService.CurrentUser.ID,
            //    ConstantHelper.InitialPostsNum)).Reverse());
            IsCommentListEmpty = (CurrentPost.NumOfComments == 0) ? 0 : 8; // int values taken from ViewStates enum
        }
    }
}
