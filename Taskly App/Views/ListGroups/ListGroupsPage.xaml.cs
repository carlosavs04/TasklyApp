using System.Collections.ObjectModel;
using Taskly_App.Helpers;
using Taskly_App.ViewModels;
using Taskly_App.Views.JoinGroup;
using Taskly_App.Views.NewGroup;
using Taskly_App.Views.Tabs.MyTasks;

namespace Taskly_App.Views.ListGroups
{
    public partial class ListGroupsPage : ContentPage
    {
        private readonly IServiceProvider _serviceProvider;
        private TeamsViewModel _viewModel;

        public ListGroupsPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            var locator = serviceProvider.GetRequiredService<ViewModelLocator>();
            _viewModel = locator.TeamsViewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadTeamsCommand.Execute(null);
        }
        private async void OnAddGroupButtonClicked(object sender, EventArgs e)
        {
             await Navigation.PushAsync(new JoinGroupPage(_serviceProvider));
        }

        private async void NavigateToHome(object sender, TappedEventArgs args)
        {
            if (args.Parameter is Models.Team selected)
            {
                _viewModel.OnSelectTeamCommand.Execute(selected);
                await Navigation.PushAsync(new AppShell(_serviceProvider));
            }
        }
    }
}
