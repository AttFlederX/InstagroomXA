using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.Navigation;

using InstagroomXA.Core.Contracts;
using InstagroomXA.Core.ViewModels;

namespace InstagroomXA.Core.ViewModels
{
    /// <summary>
    /// View model for the tab container page 
    /// </summary>
    public class MasterTabControlViewModel : BaseViewModel
    {
        private readonly IUserDataService _userDataService;

        private readonly Lazy<FeedViewModel> _feedViewModel;
        private readonly Lazy<SearchPeopleViewModel> _searchViewModel;
        private readonly Lazy<NewPostViewModel> _newPostViewModel;
        private readonly Lazy<NotificationsViewModel> _notificationsViewModel;
        private readonly Lazy<ProfileViewModel> _profileViewModel;

        public FeedViewModel FeedVM { get => _feedViewModel.Value; }
        public SearchPeopleViewModel SearchVM { get => _searchViewModel.Value; }
        public NewPostViewModel NewPostVM { get => _newPostViewModel.Value; }
        public NotificationsViewModel NotificationsVM { get => _notificationsViewModel.Value; }
        public ProfileViewModel ProfileVM { get => _profileViewModel.Value; }

        #region Commands
        public IMvxCommand SignOutCommand
        {
            get => new MvxCommand(async () =>
            {
                _userDataService.CurrentUser = null;

                // clears the navigation stack
                ShowViewModel<LoginViewModel>(presentationBundle: new MvxBundle(new Dictionary<string, string> { { "ClearStack", "" } }));
            });
        }
        #endregion

        public MasterTabControlViewModel(IMvxMessenger messenger, IUserDataService userDataService) : base(messenger)
        {
            _userDataService = userDataService;

            _feedViewModel = new Lazy<FeedViewModel>(Mvx.IocConstruct<FeedViewModel>);
            _searchViewModel = new Lazy<SearchPeopleViewModel>(Mvx.IocConstruct<SearchPeopleViewModel>);
            _newPostViewModel = new Lazy<NewPostViewModel>(Mvx.IocConstruct<NewPostViewModel>);
            _notificationsViewModel = new Lazy<NotificationsViewModel>(Mvx.IocConstruct<NotificationsViewModel>);
            _profileViewModel = new Lazy<ProfileViewModel>(Mvx.IocConstruct<ProfileViewModel>);

            // _profileViewModel.Value.Start();
        }
    }
}
