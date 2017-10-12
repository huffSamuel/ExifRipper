using Caliburn.Micro;
using ExifRipper.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ExifRipper.UI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();

            container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            container
                .PerRequest<MainViewModel>()
                .PerRequest<ShellViewModel>();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            Dictionary<string, object> windowSettings = new Dictionary<string, object>();
            windowSettings.Add("MinHeight", 480);
            windowSettings.Add("MinWidth", 640);
            windowSettings.Add("Height", 480);
            windowSettings.Add("Width", 640);
            windowSettings.Add("MaxHeight", 480);
            windowSettings.Add("MaxWidth", 640);
            windowSettings.Add("ResizeMode", ResizeMode.NoResize);

            DisplayRootViewFor<ShellViewModel>(windowSettings);
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(e.Exception.Message, "An error has occurred", MessageBoxButton.OK);
        }
    }
}
