using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstagroomXA.Core.Contracts;
using InstagroomXA.Core.Model;
using InstagroomXA.Core.Messages;

using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace InstagroomXA.Core.ViewModels
{
    public class EditProfileViewModel : BaseViewModel
    {
        private readonly IUserDataService _userDataService;
        private readonly IPostDataService _postDataService;
        private readonly IDialogService _dialogService;
        private readonly IValidationService _validationService;

        private User _currentUser;

        private string _firstName;
        private string _lastName;
        private string _email;
        private string _imagePath;

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

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RaisePropertyChanged(() => FirstName);
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                RaisePropertyChanged(() => LastName);
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                RaisePropertyChanged(() => ImagePath);
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

            });
        }

        public IMvxCommand SaveChangesCommand
        {
            get => new MvxCommand(async () =>
            {
                if (string.IsNullOrWhiteSpace(FirstName))
                {
                    _dialogService.ShowPopupMessage("Please enter your first name");
                    return;
                }
                else if (string.IsNullOrWhiteSpace(LastName))
                {
                    _dialogService.ShowPopupMessage("Please enter your last name");
                    return;
                }
                else if (!_validationService.IsEmailValid(Email))
                {
                    _dialogService.ShowPopupMessage("Please enter a valid email address");
                }
                else
                {
                    CurrentUser.FirstName = FirstName;
                    CurrentUser.LastName = LastName;
                    CurrentUser.Email = Email;
                    CurrentUser.ImagePath = ImagePath;

                    await UpdatePosts();

                    await _userDataService.UpdateUserAsync(CurrentUser);
                    Messenger.Publish(new CurrentUserUpdatedMessage(this));
                    // close the page after the user presses OK
                    _dialogService.ShowPopupMessage("Your account has been updated");
                    Close(this);
                }
            });
        }
        #endregion

        public EditProfileViewModel(IMvxMessenger messenger, IUserDataService userDataService, IPostDataService postDataService,
            IDialogService dialogService, IValidationService validationService) : base(messenger)
        {
            _userDataService = userDataService;
            _postDataService = postDataService;
            _dialogService = dialogService;
            _validationService = validationService;

            CurrentUser = _userDataService.CurrentUser;

            FirstName = CurrentUser.FirstName;
            LastName = CurrentUser.LastName;
            Email = CurrentUser.Email;
            ImagePath = CurrentUser.ImagePath;
        }


        private async Task UpdatePosts()
        {
            var userPosts = await _postDataService.GetUserPosts(CurrentUser.ID);
            foreach (var post in userPosts)
            {
                post.UserImagePath = ImagePath;
                await _postDataService.UpdatePostAsync(post);
            }
            Messenger.Publish(new PostUpdatedMessage(this));
        }
    }
}
