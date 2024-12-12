using System.Collections.ObjectModel;
using Taskly_App.Views.Tasks.New;
using Taskly_App.Views.Tasks.Edit;
using Taskly_App.Views.Tasks.Detail;
using Taskly_App.ViewModels;
using Taskly_App.Helpers;
using Taskly_App.Models;

namespace Taskly_App.Views.Tabs.AllTasks
{
    public partial class AllTasksPage : ContentPage
    {
        private IServiceProvider _serviceProvider;
        private TasksViewModel _viewModel;

        public AllTasksPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            var locator = serviceProvider.GetRequiredService<ViewModelLocator>();
            _viewModel = locator.TasksViewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadTasksCommand.Execute(null);
        }

        private async void OnAddTaskClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewTaskPage(_serviceProvider));
        }

        private async void OnDetailSelected(object sender, TappedEventArgs e)
        {
            if (e.Parameter is Note selected)
            {
                var taskId = selected.Id;
                await Navigation.PushAsync(new DetailTaskPage(_serviceProvider, taskId));
            }
        }
    }
}
