using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstagroomXA.Core.Contracts;
using InstagroomXA.Core.Helpers;
using InstagroomXA.Core.Model;

using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;

namespace InstagroomXA.Core.ViewModels
{
    /// <summary>
    /// View model for the search tab
    /// Used to find other people on Instagroom
    /// </summary>
    public class SearchPeopleViewModel : BaseViewModel
    {
        private readonly IUserDataService _userDataService;

        private MvxObservableCollection<User> _queryResult;
        private string _searchQuery;

        #region Bindable properties
        public MvxObservableCollection<User> QueryResult
        {
            get => _queryResult;
            set
            {
                _queryResult = value;
                RaisePropertyChanged(() => QueryResult);
            }
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                RaisePropertyChanged(() => SearchQuery);

                QueryChangedCommand.Execute();
            }
        }
        #endregion

        #region Commands 
        public IMvxCommand QueryChangedCommand
        {
            get => new MvxCommand(async () =>
            {
                if (!string.IsNullOrEmpty(SearchQuery) && SearchQuery.Length >= ConstantHelper.MinQueryLength)
                {
                    var result = new MvxObservableCollection<User>(await _userDataService.GetUserByQueryAsync(SearchQuery.ToLowerInvariant()));
                    result.Remove(_userDataService.CurrentUser); // remove the current user from the result
                    QueryResult = result;
                }
                else { QueryResult = new MvxObservableCollection<User>(); }  // empty the result
            });
        }

        public IMvxCommand UserSelectedCommand
        {
            get => new MvxCommand<User>(async selectedUser =>
            {
                if (selectedUser.ID != _userDataService.CurrentUser.ID)
                {
                    ShowViewModel<UserProfileViewModel>(new { userId = selectedUser.ID });
                }
            }); 
        }
        #endregion

        public SearchPeopleViewModel(IMvxMessenger messenger, IUserDataService userDataService) : base(messenger)
        {
            _userDataService = userDataService;
        }


        public class SavedState
        {
            public MvxObservableCollection<User> QueryResult { get; set; }
            public string SearchQuery { get; set; }
        }

        public SavedState SaveState()
        {
            return new SavedState { QueryResult = this.QueryResult, SearchQuery = this.SearchQuery };
        }

        public void ReloadState(SavedState savedState)
        {
            QueryResult = savedState.QueryResult;
            SearchQuery = savedState.SearchQuery;
        }
    }
}
