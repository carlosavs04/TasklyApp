using Taskly_App.Helpers;
using Taskly_App.ViewModels;
using Taskly_App.Views.JoinGroup;

namespace Taskly_App.Views.Register
{
    public partial class RegisterPage : ContentPage
    {
        private readonly IServiceProvider _serviceProvider;
        private RegisterViewModel _viewModel;

        public RegisterPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            var locator = serviceProvider.GetRequiredService<ViewModelLocator>();
            _viewModel = locator.RegisterViewModel;
            BindingContext = _viewModel;
        }
    }
}