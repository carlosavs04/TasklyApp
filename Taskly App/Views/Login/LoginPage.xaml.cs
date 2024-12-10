using Taskly_App.Helpers;
using Taskly_App.ViewModels;
using Taskly_App.Views.Login;
using Taskly_App.Views.Register;
using Taskly_App.Views.Login.ForgotPassword;

namespace Taskly_App.Views.Login
{
    public partial class LoginPage : ContentPage
    {
        private readonly IServiceProvider _serviceProvider;
        private LoginViewModel _viewModel;

        public LoginPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            var locator = serviceProvider.GetRequiredService<ViewModelLocator>();
            _viewModel = locator.LoginViewModel;
            BindingContext = _viewModel;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage(_serviceProvider));
        }
        private async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            var forgotPasswordPage = new ForgotPasswordPage(_serviceProvider);
            await Navigation.PushModalAsync(forgotPasswordPage);
        }
    }

}
