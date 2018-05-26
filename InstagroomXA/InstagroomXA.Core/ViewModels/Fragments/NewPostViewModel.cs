using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstagroomXA.Core.Contracts;

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

        private string _description;

        #region Bindable properties
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
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
                
            });
        }
        #endregion

        public NewPostViewModel(IMvxMessenger messenger, IUserDataService userDataService, IPostDataService postDataService, 
            IDialogService dialogService) : base(messenger)
        {
            _userDataService = userDataService;
            _postDataService = postDataService;
            _dialogService = dialogService;
        }
    }
}
