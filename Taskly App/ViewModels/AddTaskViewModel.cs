using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taskly_App.Models;
using Taskly_App.Models.Config;
using Taskly_App.Models.Requests;
using Taskly_App.Services;
using Taskly_App.Views.Tabs.AllTasks;

namespace Taskly_App.ViewModels
{
    public class AddTaskViewModel : BaseViewModel
    {
        private readonly ApiRouterService _routerService;
        private readonly NavigationService _navigationService;
        private readonly ConfigurationService _configurationService;
        private string _title = string.Empty;
        private string _body = string.Empty;
        private string _errorMessage = string.Empty;
        private int _responsibleId = 0;
        private int _teamId = 0;
        private bool _isBusy = false;
        private User _selectedUser;
        private Dictionary<string, List<string>>? _errors;
        private ObservableCollection<User> _users = new ObservableCollection<User>();

        public AddTaskViewModel(ApiRouterService routerService, NavigationService navigationService, ConfigurationService configurationService)
        {
            _routerService = routerService;
            _navigationService = navigationService;
            _configurationService = configurationService;
            _teamId = configurationService.GetSelectedTeamId();
            LoadUsersCommand = new Command(async () => await LoadUsersAsync());
            AddTaskCommand = new Command(async () => await AddTaskAsync(), CanAddTask);
        }

        public ObservableCollection<User> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }

        public string Title
        {
            get => _title;
            set
            {
                if (SetProperty(ref _title, value))
                    ((Command)AddTaskCommand).ChangeCanExecute();
            }
        }

        public string Body
        {
            get => _body;
            set
            {
                if (SetProperty(ref _body, value))
                    ((Command)AddTaskCommand).ChangeCanExecute();
            }
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (SetProperty(ref _selectedUser, value))
                {
                    _responsibleId = value.Id;
                    ((Command)AddTaskCommand).ChangeCanExecute();
                }
            }
        }

        public string ErrorMessage
        {
            get
            {
                if (_errors != null && _errors.Any())
                {
                    var errorsList = _errors.SelectMany(kv => kv.Value.Select(v => $"- {kv.Key}: {v}")).ToList();
                    return $"{_errorMessage}:\n{string.Join("\n", errorsList)}";
                }
                return _errorMessage;
            }
            set => SetProperty(ref _errorMessage, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (SetProperty(ref _isBusy, value))
                    ((Command)AddTaskCommand).ChangeCanExecute();
            }
        }

        public ICommand LoadUsersCommand { get; }
        public ICommand AddTaskCommand { get; }

        public async Task LoadUsersAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var response = await _routerService.GetUsersByTeam(_teamId);

                if (response != null && response.Status == "success" && response.Data != null && response.Data.Count > 0)
                {
                    var userList = response.Data as List<User>;
                    if (userList != null)
                        Users = new ObservableCollection<User>(userList);
                }
                else
                {
                    ErrorMessage = "Parece que aún no hay usuarios en tu equipo";
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

        public async Task AddTaskAsync()
        {
            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var request = new NoteRequest
                {
                    Title = Title,
                    Body = Body,
                    ResponsibleId = _responsibleId,
                    TeamId = _teamId
                };

                var response = await _routerService.CreateTask(request);

                if (response != null && response.Status == "success")
                {
                    await _navigationService.NavigateToAsync<AllTasksPage>();
                }
                else
                {
                    ErrorMessage = response?.Message ?? "Error al crear tarea";
                }
            }
            catch (ApiException ex)
            {
                _errors = ex.Errors as Dictionary<string, List<string>>;
                ErrorMessage = ex.Message ?? "Error desconocido";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanAddTask()
        {
            return !string.IsNullOrWhiteSpace(Title)
                && !string.IsNullOrWhiteSpace(Body)
                && _responsibleId > 0
                && !IsBusy;
        }
    }
}
