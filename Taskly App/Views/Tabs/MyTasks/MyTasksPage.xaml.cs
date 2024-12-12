using Taskly_App.Views.Tabs.MyTasks;
using System.Collections.ObjectModel;
using Taskly_App.Views.Tasks.New;
using Taskly_App.Views.Tasks.Edit;
using Taskly_App.Views.Tasks.Detail;
using Taskly_App.ViewModels;
using Taskly_App.Helpers;
using Taskly_App.Models;

namespace Taskly_App.Views.Tabs.MyTasks
{
    public partial class MyTasksPage : ContentPage
    {
        private readonly IServiceProvider _serviceProvider;
        private MyTasksViewModel _viewModel;

        public MyTasksPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            var locator = serviceProvider.GetRequiredService<ViewModelLocator>();
            _viewModel = locator.MyTasksViewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadTasksCommand.Execute(null);
        }

        private void OnTaskStatusChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.BindingContext is Note task)
            {
                if (task.IsCompleted != task.InitialIsCompleted)
                {
                    _viewModel.UpdateTaskStatusCommand.Execute(task);
                    task.InitialIsCompleted = task.IsCompleted;
                }
            }
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
