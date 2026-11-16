using Counter.ViewModels;
using System.Diagnostics;

namespace Counter
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var page = new MainPage();
            var window = new Window(page);
            
            window.Stopped += (sender, args) =>
            {
                Debug.WriteLine("stopefg");
                if (window.Page?.BindingContext is MainPageViewModel vm)
                {
                    vm.SaveCounters();
                }
            };

            window.Destroying += (sender, args) =>
            {
                Debug.WriteLine("desytu");
                if (window.Page?.BindingContext is MainPageViewModel vm)
                {
                    vm.SaveCounters();
                }
            };

            return window;
        }
    }
}