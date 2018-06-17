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
    /// View model for the 'New post' tab
    /// Contains interface for adding a new post w/ photo selection & description
    /// </summary>
    public class NewPostViewModel : BaseViewModel
    {
        private readonly IUserDataService _userDataService;
        private readonly IPostDataService _postDataService;
        private readonly IDialogService _dialogService;

        private Post _newPost;

        #region Bindable properties
        public Post NewPost
        {
            get => _newPost;
            set
            {
                _newPost = value;
                RaisePropertyChanged(() => NewPost);
            }
        }

        //public string Description
        //{
        //    get => _description;
        //    set
        //    {
        //        _description = value;
        //        RaisePropertyChanged(() => Description);
        //    }
        //}
        #endregion

        #region Commands 
        public IMvxCommand TakePhotoCommand
        {
            get => new MvxCommand(() =>
            {
                
            });
        }

        public IMvxCommand OpenGalleryCommand
        {
            get => new MvxCommand(() =>
            {

            });
        }

        public IMvxCommand PostCommand
        {
            get => new MvxCommand(async () =>
            {
                if (!string.IsNullOrEmpty(NewPost.ImagePath))
                {
                    // add a new post
                    var newPost = new Post()
                    {
                        Description = NewPost.Description,
                        ImagePath = NewPost.ImagePath,
                        ThumbnailPath = NewPost.ThumbnailPath,
                        Likes = 0,
                        PostTime = DateTime.Now,
                        UserID = _userDataService.CurrentUser.ID,
                        NumOfComments = 0,
                        Username = _userDataService.CurrentUser.Username,
                        UserImagePath = _userDataService.CurrentUser.ImagePath
                    };

                    await _postDataService.AddPostAsync(newPost);
                    _userDataService.CurrentUser.NumOfPosts++;
                    await _userDataService.UpdateUserAsync(_userDataService.CurrentUser);

                    NewPost = new Post()
                    {
                        ImagePath = string.Empty,
                        ThumbnailPath = string.Empty,
                        Description = string.Empty
                    };
                    

                    _dialogService.ShowPopupMessage("Your post has been published successfully");
                }
                else { _dialogService.ShowPopupMessage("Please add an image"); }
            });
        }
        #endregion

        public NewPostViewModel(IMvxMessenger messenger, IUserDataService userDataService, IPostDataService postDataService, 
            IDialogService dialogService) : base(messenger)
        {
            _userDataService = userDataService;
            _postDataService = postDataService;
            _dialogService = dialogService;

            NewPost = new Post()
            {
                ImagePath = string.Empty,
                ThumbnailPath = string.Empty,
                Description = string.Empty
            };
        }


        public class SavedState
        {
            public Post NewPost { get; set; }
        }

        public SavedState SaveState()
        {
            return new SavedState { NewPost = this.NewPost };
        }

        public void ReloadState(SavedState savedState)
        {
            NewPost = savedState.NewPost;
        }
    }
}
