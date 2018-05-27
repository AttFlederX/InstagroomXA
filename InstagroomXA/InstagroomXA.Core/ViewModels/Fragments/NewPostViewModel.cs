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
                // fetch the data from Facebook API
            });
        }

        public IMvxCommand PostCommand
        {
            get => new MvxCommand(() =>
            {
                if (!string.IsNullOrEmpty(NewPost.ImagePath))
                {
                    // add a new post
                    var newPost = new Post()
                    {
                        Description = NewPost.Description,
                        ImagePath = NewPost.ImagePath,
                        Likes = 0,
                        PostTime = DateTime.Now,
                        UserID = _userDataService.CurrentUser.ID
                    };

                    _postDataService.AddPostAsync(newPost);
                    NewPost = new Post()
                    {
                        Description = string.Empty
                    };

                    _dialogService.ShowAlertAsync("Your post has been published successfully", "New post", "OK");
                }
                else { _dialogService.ShowAlertAsync("Please add an image", "Error", "OK"); }
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
                Description = string.Empty
            };
        }
    }
}
