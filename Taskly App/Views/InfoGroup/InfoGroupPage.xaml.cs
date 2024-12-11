using System.ComponentModel;
using System.Runtime.CompilerServices;
using Taskly_App.Helpers;
using Taskly_App.ViewModels;

namespace Taskly_App.Views.InfoGroup
{
    public partial class InfoGroupPage : ContentPage
    {
        private IServiceProvider _serviceProvider;
        private TeamDetailsViewModel _viewModel;

        public InfoGroupPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            var locator = serviceProvider.GetRequiredService<ViewModelLocator>();
            _viewModel = locator.TeamDetailsViewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadTeamCommand.Execute(null);
        }
    }
}
