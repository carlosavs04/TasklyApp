using System.Collections.ObjectModel;
using Taskly_App.Helpers;
using Taskly_App.ViewModels;

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
    }
}
