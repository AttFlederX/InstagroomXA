using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstagroomXA.Core.Contracts;
using InstagroomXA.Core.ViewModels;

using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace InstagroomXA.Core
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        public async void Start(object hint = null)
        {
            // hardcoded login for quicker launch
            //var userService = Mvx.Resolve<IUserDataService>();
            //var user = await userService.GetUserByIDAsync(1);
            //if (user != null)
            //{
            //    userService.CurrentUser = user;
            //    ShowViewModel<MasterTabControlViewModel>();
            //}
            //else
            //{
                ShowViewModel<WelcomeViewModel>();
            //}
        }
    }
}
