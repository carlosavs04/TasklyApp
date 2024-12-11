using System.Collections.ObjectModel;
using System.Windows.Input;
using Taskly_App.Helpers;
using Taskly_App.Models;
using Taskly_App.ViewModels;

namespace Taskly_App.Views.MembersGroup
{
    public partial class MembersGroupPage : ContentPage
    {
        private IServiceProvider _serviceProvider;
        private TeamMembersViewModel _viewModel;

        public MembersGroupPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            var locator = serviceProvider.GetRequiredService<ViewModelLocator>();
            _viewModel = locator.TeamMembersViewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadMembersCommand.Execute(null);
        }
    }
}
