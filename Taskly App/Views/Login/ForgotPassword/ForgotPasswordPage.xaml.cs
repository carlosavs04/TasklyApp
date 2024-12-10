using Taskly_App.Helpers;
using Taskly_App.ViewModels;

namespace Taskly_App.Views.Login.ForgotPassword
{
    public partial class ForgotPasswordPage : ContentPage
    {
        private IServiceProvider _servicesProvider;
        private ForgotPasswordViewModel _viewModel;

        public ForgotPasswordPage(IServiceProvider servicesProvider)
        {
            InitializeComponent();
            _servicesProvider = servicesProvider;
            var locator = servicesProvider.GetRequiredService<ViewModelLocator>();
            _viewModel = locator.ForgotPasswordViewModel;
            BindingContext = _viewModel;
        }

        private async void OnCloseClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
