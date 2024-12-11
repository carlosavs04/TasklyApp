using System.Collections.ObjectModel;
using Taskly_App.Helpers;
using Taskly_App.Models;
using Taskly_App.ViewModels;

namespace Taskly_App.Views.UpdatePassword
{
    public partial class UpdatePasswordPage : ContentPage
    {
        private IServiceProvider _serviceProvider;
        private UpdatePasswordViewModel _viewModel;

        public UpdatePasswordPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            var locator = serviceProvider.GetRequiredService<ViewModelLocator>();
            _viewModel = locator.UpdatePasswordViewModel;
            BindingContext = _viewModel;
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
