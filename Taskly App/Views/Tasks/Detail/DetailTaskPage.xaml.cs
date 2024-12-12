using System.Collections.ObjectModel;
using System.Windows.Input;
using Taskly_App.Helpers;
using Taskly_App.ViewModels;

namespace Taskly_App.Views.Tasks.Detail
{
    public partial class DetailTaskPage : ContentPage
    {
        private readonly IServiceProvider _serviceProvider;
        private TaskDetailsViewModel _viewModel;
        private int _taskId;

        public DetailTaskPage(IServiceProvider serviceProvider, int taskId)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            var locator = serviceProvider.GetRequiredService<ViewModelLocator>();
            _viewModel = locator.TaskDetailsViewModel;
            BindingContext = _viewModel;
            _taskId = taskId;

            LoadTaskDetails(taskId);
        }

        private void LoadTaskDetails(int taskId)
        {
            _viewModel.Initialize(taskId);
            _viewModel.LoadTaskCommand.Execute(null);
        }
    }
}
