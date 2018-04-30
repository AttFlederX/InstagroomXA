﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

using InstagroomXA.Core.Contracts;
using InstagroomXA.Core.ViewModels;

namespace InstagroomXA.Core.ViewModels
{
    /// <summary>
    /// View model for the tab container page 
    /// </summary>
    public class MasterTabControlViewModel : MvxViewModel
    {
        private readonly Lazy<FeedViewModel> _feedViewModel;
        private readonly Lazy<SearchViewModel> _searchViewModel;
        private readonly Lazy<NewPostViewModel> _newPostViewModel;
        private readonly Lazy<NotificationsViewModel> _notificationsViewModel;
        private readonly Lazy<ProfileViewModel> _profileViewModel;

        public MasterTabControlViewModel()
        {
            _feedViewModel = new Lazy<FeedViewModel>(Mvx.IocConstruct<FeedViewModel>);
            _searchViewModel = new Lazy<SearchViewModel>(Mvx.IocConstruct<SearchViewModel>);
            _newPostViewModel = new Lazy<NewPostViewModel>(Mvx.IocConstruct<NewPostViewModel>);
            _notificationsViewModel = new Lazy<NotificationsViewModel>(Mvx.IocConstruct<NotificationsViewModel>);
            _profileViewModel = new Lazy<ProfileViewModel>(Mvx.IocConstruct<ProfileViewModel>);
        }
    }
}