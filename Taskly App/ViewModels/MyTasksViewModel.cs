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
    public class MyTasksViewModel : BaseViewModel
    {
        private readonly ApiRouterService _routerService;
        private readonly ConfigurationService _configurationService;
        private string _errorMessage = string.Empty;
        private bool _isBusy = false;
        private int _teamId;
        private ObservableCollection<Note> _tasks = new ObservableCollection<Note>();

        public MyTasksViewModel(ApiRouterService routerService, ConfigurationService configurationService)
        {
            _routerService = routerService;
            _configurationService = configurationService;
            _teamId = configurationService.GetSelectedTeamId();
            LoadTasksCommand = new Command(async () => await LoadTasksAsync());
            UpdateTaskStatusCommand = new Command<Note>(async (task) => await UpdateTaskStatusAsync(task));
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
        public ICommand UpdateTaskStatusCommand { get; }

        private async Task LoadTasksAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var response = await _routerService.GetUserTasks(_teamId);

                if (response != null && response.Status == "success" && response.Data != null && response.Data.Count > 0)
                {
                    var taskList = response.Data as List<Note>;
                    if (taskList != null)
                    {
                        foreach (var task in taskList)
                        {
                            task.InitialIsCompleted = task.IsCompleted;
                        }
                        Tasks = new ObservableCollection<Note>(taskList);
                    }
                }
                else
                {
                    ErrorMessage = "Parece que aún no tienes ninguna tarea asignada";
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

        private async Task UpdateTaskStatusAsync(Note task)
        {
            if (IsBusy || task == null)
                return;

            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var response = task.IsCompleted ? await _routerService.MarkTaskAsCompleted(task.Id) : await _routerService.UnmarkTaskAsCompleted(task.Id);

                if (response != null && response.Status == "success")
                {
                    var updatedTasks = Tasks.ToList();
                    var updatedTask = updatedTasks.FirstOrDefault(t => t.Id == task.Id);

                    if (updatedTask != null)
                        updatedTask.IsCompleted = task.IsCompleted;

                    Tasks = new ObservableCollection<Note>(updatedTasks);

                    if (Application.Current?.MainPage != null)
                        await Application.Current.MainPage.DisplayAlert("Éxito", "Tarea actualizada correctamente.", "OK");
                }
                else
                {
                    ErrorMessage = "No se pudo actualizaar la tarea";
                    task.IsCompleted = !task.IsCompleted;
                }
            }
            catch (ApiException ex)
            {
                ErrorMessage = ex.Message;
                task.IsCompleted = !task.IsCompleted;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
