using Taskly_App.Helpers;
using Taskly_App.ViewModels;
using Taskly_App.Views.ListGroups;
using Taskly_App.Views.Tabs.Settings;

namespace Taskly_App.Views.Tabs.Settings
{
    public partial class SettingsPage : ContentPage
    {
        private IServiceProvider _serviceProvider;
        private SettingsViewModel _viewModel;

        public SettingsPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            var locator = serviceProvider.GetRequiredService<ViewModelLocator>();
            _viewModel = locator.SettingsViewModel;
            BindingContext = _viewModel;
        }

        private void NavigateToGroupList(object sender, EventArgs e)
        {
            if (Application.Current != null)
            {
                var app = (App)Application.Current;
                app.RestartApp();
            }
        }
    }
}