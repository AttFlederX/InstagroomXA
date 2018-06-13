using Android.Content;
using Android.Widget;

using InstagroomXA.Core.Contracts;
using InstagroomXA.Droid.Presenters;
using InstagroomXA.Droid.Services;

using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;

namespace InstagroomXA.Droid
{
    public class Setup : MvxAppCompatSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        /// <summary>
        /// For platform-dependent services
        /// </summary>
        protected override void InitializeIoC()
        {
            base.InitializeIoC();

            Mvx.RegisterSingleton<IDialogService>(() => new DialogService());
            Mvx.RegisterSingleton<IDBConnectionService>(() => new DBConnectionService());
            Mvx.RegisterSingleton<IEnumService>(() => new EnumService());
            // Mvx.RegisterSingleton<ICameraService>(() => new CameraService());
        }

        /// <summary>
        /// For custom bindings
        /// </summary>
        /// <param name="registry"></param>
        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            base.FillTargetFactories(registry);

            registry.RegisterFactory(new MvxCustomBindingFactory<TextView>("TextStyle", textView => new TextStyleCustomBinding(textView)));
        }

        /// <summary>
        /// For custom view presenters
        /// </summary>
        /// <returns></returns>
        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var presenter = new ClearStackCustomPresenter();
            Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);
            return presenter;
        }
    }
}
