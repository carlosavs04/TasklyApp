
using Taskly_App.Helpers;
using Taskly_App.ViewModels;

namespace Taskly_App.Views.NewGroup
{
    public partial class NewGroupPage : ContentPage
    {
        private readonly IServiceProvider _serviceProvider;
        private AddTeamViewModel _viewModel;

        public NewGroupPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            var locator = serviceProvider.GetRequiredService<ViewModelLocator>();
            _viewModel = locator.AddTeamViewModel;
            BindingContext = _viewModel;
        }
    }
}
