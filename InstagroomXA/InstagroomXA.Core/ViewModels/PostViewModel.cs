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
    /// View model for post details (description, comments, likes)
    /// </summary>
    public class PostViewModel : BaseViewModel
    {
        private readonly IEnumService _enumService;
        private readonly IDialogService _dialogService;
        private readonly IUserDataService _userDataService;
        private readonly IPostDataService _postDataService;
        private readonly ICommentDataService _commentDataService;
        private readonly INotificationDataService _notificationDataService;

        private int _postId;
        // private int _userId;

        private User _currentUser;
        private MvxObservableCollection<Comment> _commentList;
        private Post _currentPost;
        private int _isCommentListEmpty;
        private string _commentText;
        private int _isPostLikedByUser;

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

        public int IsPostLikedByUser // for # of likes textview 
        {
            get => _isPostLikedByUser;
            set
            {
                _isPostLikedByUser = value;
                RaisePropertyChanged(() => IsPostLikedByUser);
            }
        }

        #endregion

        #region Commands 
        public IMvxCommand LikeCommand
        {
            get => new MvxCommand(async () =>
            {
                if (!CurrentUser.LikedPosts.Contains(CurrentPost.ID))
                {
                    CurrentUser.LikedPosts.Add(CurrentPost.ID);
                    await _userDataService.UpdateUserAsync(CurrentUser);

                    CurrentPost.Likes++;
                    RaisePropertyChanged(() => CurrentPost); // update the likes count
                    await _postDataService.UpdatePostAsync(CurrentPost);

                    IsPostLikedByUser = _enumService.TypefaceStyleBold;
                    _dialogService.ShowPopupMessage("You've liked this post");

                    // send a notification to post's author
                    var likeNotif = new Notification()
                    {
                        PostID = CurrentPost.ID,
                        SourceUserID = CurrentUser.ID,
                        TargetUserID = CurrentPost.UserID,
                        Text = string.Format(ConstantHelper.LikeNotificationText, CurrentUser.Username),
                        Time = DateTime.Now,
                        SourceUserImagePath = CurrentUser.ImagePath
                    };
                    if (likeNotif.SourceUserID != likeNotif.TargetUserID)
                    { await _notificationDataService.AddNotificationAsync(likeNotif); }
                }
                else
                {
                    CurrentUser.LikedPosts.Remove(CurrentPost.ID);
                    await _userDataService.UpdateUserAsync(CurrentUser);

                    CurrentPost.Likes--;
                    RaisePropertyChanged(() => CurrentPost); // update the likes count
                    await _postDataService.UpdatePostAsync(CurrentPost);

                    IsPostLikedByUser = _enumService.TypefaceStyleNormal;
                    _dialogService.ShowPopupMessage("You've unliked this post");
                }

                Messenger.Publish(new PostUpdatedMessage(this) { PostID = CurrentPost.ID });
            });
        }

        public IMvxCommand CommentSelectedCommand
        {
            get => new MvxCommand<Comment>(async selectedComment =>
            {
                if (selectedComment.CommenterID != CurrentUser.ID)
                {
                    ShowViewModel<UserProfileViewModel>(new { userId = selectedComment.CommenterID });
                }
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
                    CommentList.Insert(0, newComment);
                    IsCommentListEmpty = _enumService.ViewStateGone;

                    CurrentPost.NumOfComments++;
                    await _postDataService.UpdatePostAsync(CurrentPost);
                    RaisePropertyChanged(() => CurrentPost); // update the comments count
                    Messenger.Publish(new PostUpdatedMessage(this) { PostID = CurrentPost.ID });

                    // send a notification to post's author
                    var commentNotif = new Notification()
                    {
                        PostID = CurrentPost.ID,
                        SourceUserID = CurrentUser.ID,
                        TargetUserID = CurrentPost.UserID,
                        Text = string.Format(ConstantHelper.CommentNotificationText, CurrentUser.Username),
                        Time = DateTime.Now,
                        SourceUserImagePath = CurrentUser.ImagePath
                    };
                    if (commentNotif.SourceUserID != commentNotif.TargetUserID)
                    { await _notificationDataService.AddNotificationAsync(commentNotif); }

                    CommentText = string.Empty;
                }
                else { await _dialogService.ShowAlertAsync("Please enter the comment's text", "Error", "OK"); }
            });
        }
        #endregion

        public PostViewModel(IMvxMessenger messenger, IEnumService enumService, IDialogService dialogService, 
            IUserDataService userDataService, IPostDataService postDataService, ICommentDataService commentDataService, 
            INotificationDataService notificationDataService) : base(messenger)
        {
            _enumService = enumService;
            _dialogService = dialogService;
            _userDataService = userDataService;
            _postDataService = postDataService;
            _commentDataService = commentDataService;
            _notificationDataService = notificationDataService;

            CurrentUser = _userDataService.CurrentUser;
        }


        public void Init(int postId)
        {
            _postId = postId;
            // _userId = userId;
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
            // CurrentUser = (_userId == -1) ? _userDataService.CurrentUser : await _userDataService.GetUserByIDAsync(_userId);
            CurrentUser = _userDataService.CurrentUser;
            CurrentPost = await _postDataService.GetPostByIDAsync(_postId);
            CommentList = new MvxObservableCollection<Comment>((await _commentDataService.GetPostComments(CurrentPost.ID)).Reverse());
            IsCommentListEmpty = (CurrentPost.NumOfComments == 0) ? _enumService.ViewStateVisible : 
                _enumService.ViewStateGone;
            CommentText = string.Empty;
            IsPostLikedByUser = (CurrentUser.LikedPosts.Contains(CurrentPost.ID)) ? _enumService.TypefaceStyleBold :
                _enumService.TypefaceStyleNormal; // # of likes is in bold if the user liked the post
        }
    }
}
