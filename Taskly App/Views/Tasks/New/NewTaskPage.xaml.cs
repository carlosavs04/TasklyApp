using Taskly_App.Views.Tasks.New;
using System.Collections.ObjectModel;
using Taskly_App.ViewModels;
using Taskly_App.Helpers;

namespace Taskly_App.Views.Tasks.New
{
    public partial class NewTaskPage : ContentPage
    {
        private IServiceProvider _serviceProvider;
        private AddTaskViewModel _viewModel;

        public NewTaskPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            var locator = serviceProvider.GetRequiredService<ViewModelLocator>();
            _viewModel = locator.AddTaskViewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadUsersCommand.Execute(null);
        }
    }
}