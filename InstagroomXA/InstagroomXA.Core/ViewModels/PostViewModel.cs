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
        private readonly IDialogService _dialogService;
        private readonly IUserDataService _userDataService;
        private readonly IPostDataService _postDataService;
        private readonly ICommentDataService _commentDataService;

        private int _postId;
        private int _userId;

        private User _currentUser;
        private MvxObservableCollection<Comment> _commentList;
        private Post _currentPost;
        private int _isCommentListEmpty;
        private string _commentText;

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

        public MvxObservableCollection<Comment> CommentList
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

        public string CommentText
        {
            get => _commentText;
            set
            {
                _commentText = value;
                RaisePropertyChanged(() => CommentText);
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

        public IMvxCommand CommentSelectedCommand
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
                if (!string.IsNullOrEmpty(CommentText))
                {
                    var newComment = new Comment()
                    {
                        CommenterID = CurrentUser.ID,
                        CommenterUsername = CurrentUser.Username,
                        PostID = CurrentPost.ID,
                        PostTime = DateTime.Now,
                        Text = CommentText
                    };

                    await _commentDataService.AddCommentAsync(newComment);
                    CommentList.Add(newComment);

                    CurrentPost.NumOfComments++;
                    await _postDataService.UpdatePostAsync(CurrentPost);

                    CommentText = string.Empty;
                }
                else { await _dialogService.ShowAlertAsync("Please enter the comment's text", "Error", "OK"); }
            });
        }
        #endregion

        public PostViewModel(IMvxMessenger messenger, IDialogService dialogService, IUserDataService userDataService,
            IPostDataService postDataService, ICommentDataService commentDataService) : base(messenger)
        {
            _dialogService = dialogService;
            _userDataService = userDataService;
            _postDataService = postDataService;
            _commentDataService = commentDataService;

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
            CommentList = new MvxObservableCollection<Comment>((await _commentDataService.GetPostComments(CurrentPost.ID)));
            IsCommentListEmpty = (CurrentPost.NumOfComments == 0) ? 0 : 8; // int values taken from ViewStates enum
            CommentText = string.Empty;
        }
    }
}
