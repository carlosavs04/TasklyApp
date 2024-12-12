using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taskly_App.Models;
using Taskly_App.Models.Config;
using Taskly_App.Services;
using Taskly_App.Views.Tabs.AllTasks;
using Taskly_App.Views.Tabs.MyTasks;

namespace Taskly_App.ViewModels
{
    public class TaskDetailsViewModel : BaseViewModel
    {
        private readonly ApiRouterService _apiRouterService;
        private readonly NavigationService _navigationService;
        private Note? _note;
        private string _errorMessage = string.Empty;    
        private bool _isBusy = false;
        private bool _fromMyTasks = false;
        private int _taskId;

        public TaskDetailsViewModel(ApiRouterService apiRouterService, NavigationService navigationService)
        {
            _apiRouterService = apiRouterService;
            _navigationService = navigationService;
            LoadTaskCommand = new Command(async () => await LoadTaskAsync());
            DeleteTaskCommand = new Command(async () => await DeleteTaskAsync());
        }

        public Note? Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
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

        public void Initialize(int taskId, bool fromMyTasks = false)
        {
            _taskId = taskId;
            _fromMyTasks = fromMyTasks;
        }

        public ICommand LoadTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        private async Task LoadTaskAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                var response = await _apiRouterService.GetTask(_taskId);

                if (response != null && response.Status == "success" && response.Data != null)
                {
                    string responsibleUsername = string.Empty, creatorUsername = string.Empty;

                    var responsibleUser = await _apiRouterService.GetUser(response.Data.ResponsibleId);
                    if (responsibleUser != null && responsibleUser.Status == "success" && responsibleUser.Data != null)
                        responsibleUsername = responsibleUser.Data.Username;

                    var creatorUser = await _apiRouterService.GetUser(response.Data.CreatedBy);
                    if (creatorUser != null && creatorUser.Status == "success" && creatorUser.Data != null)
                        creatorUsername = creatorUser.Data.Username;

                    Note = new Note
                    {
                        Id = response.Data.Id,
                        Title = response.Data.Title,
                        Body = response.Data.Body,
                        IsCompleted = response.Data.IsCompleted,
                        CompletedAt = response.Data.CompletedAt,
                        CreatedAt = response.Data.CreatedAt,
                        ResponsibleId = response.Data.ResponsibleId,
                        TeamId = response.Data.TeamId,
                        CreatedBy = response.Data.CreatedBy,
                        ResponsibleUsername = responsibleUsername,
                        CreatorUsername = creatorUsername
                    };
                }
                else
                {
                    ErrorMessage = "No se pudo cargar la información de la tarea";
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

        private async Task DeleteTaskAsync()
        {
            if (IsBusy || Note == null)
                return;

            bool confirm = false;

            if (Application.Current?.MainPage != null)
                confirm = await Application.Current.MainPage.DisplayAlert("Eliminar tare", "¿Estás seguro de eliminar esta tarea?", "Sí", "No");

            if (!confirm)
                return;

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                var response = await _apiRouterService.RemoveTask(_taskId);

                if (response != null && response.Status == "success")
                {
                    await _navigationService.NavigateToAsync<AppShell>();
                }
                else
                {
                    ErrorMessage = "No se pudo eliminar la tarea";
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
