using Taskly_App.Helpers;
using Taskly_App.ViewModels;
using Taskly_App.Views.ListGroups;
using Taskly_App.Views.NewGroup;

namespace Taskly_App.Views.JoinGroup
{
    public partial class JoinGroupPage : ContentPage
    {
        private readonly IServiceProvider _serviceProvider;
        private JoinTeamViewModel _viewModel;

        public JoinGroupPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            var locator = serviceProvider.GetRequiredService<ViewModelLocator>();
            _viewModel = locator.JoinTeamViewModel;
            BindingContext = _viewModel;
        }

        private async void OnGoToCreateGroup(object sender, EventArgs e) {
            await Navigation.PushAsync(new NewGroupPage(_serviceProvider));
        }
    }
}