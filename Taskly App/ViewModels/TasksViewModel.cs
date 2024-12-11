using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taskly_App.Models;
using Taskly_App.Models.Config;
using Taskly_App.Services;

namespace Taskly_App.ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        private readonly ApiRouterService _routerService;
        private readonly ConfigurationService _configurationService;
        private string _errorMessage = string.Empty;
        private bool _isBusy = false;
        private int _teamId;
        private ObservableCollection<Note> _tasks = new ObservableCollection<Note>();

        public TasksViewModel(ApiRouterService routerService, ConfigurationService configurationService)
        {
            _routerService = routerService;
            _configurationService = configurationService;
            _teamId = configurationService.GetSelectedTeamId();
            LoadTasksCommand = new Command(async () => await LoadTasksAsync());
        }

        public ObservableCollection<Note> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public ICommand LoadTasksCommand { get; }

        private async Task LoadTasksAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var response = await _routerService.GetTeamTasks(_teamId);

                if (response != null && response.Status == "success" && response.Data != null && response.Data.Count > 0)
                {
                    var taskList = response.Data as List<Note>;
                    if (taskList != null)
                    {
                        foreach (var task in taskList)
                        {
                            var responsibleUser = await _routerService.GetUser(task.ResponsibleId);
                            if (responsibleUser != null && responsibleUser.Status == "success" && responsibleUser.Data != null)
                            {
                                task.ResponsibleUsername = $"Responsable: {responsibleUser.Data.Username}";
                            }
                        }
                        Tasks = new ObservableCollection<Note>(taskList);
                    }
                        
                }
                else
                {
                    ErrorMessage = "No se encuentra ninguna tarea disponible.";
                }
            }
            catch (ApiException ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
