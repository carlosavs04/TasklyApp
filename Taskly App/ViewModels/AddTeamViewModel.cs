using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taskly_App.Models.Config;
using Taskly_App.Models.Requests;
using Taskly_App.Services;
using Taskly_App.Views.ListGroups;

namespace Taskly_App.ViewModels
{
    public class AddTeamViewModel : BaseViewModel
    {
        private readonly ApiRouterService _routerService;
        private readonly NavigationService _navigationService;
        private string _name = string.Empty;
        private string _description = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isBusy = false;
        private Dictionary<string, List<string>>? _errors;

        public AddTeamViewModel(ApiRouterService routerService, NavigationService navigationService)
        {
            _routerService = routerService;
            _navigationService = navigationService;
            AddTeamCommand = new Command(async () => await AddTeamAsync(), CanAddTeam);
        }

        public string Name
        {
            get => _name;
            set
            {
                if (SetProperty(ref _name, value))
                    ((Command)AddTeamCommand).ChangeCanExecute();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (SetProperty(ref _description, value))
                    ((Command)AddTeamCommand).ChangeCanExecute();
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
                    ((Command)AddTeamCommand).ChangeCanExecute();
            }
        }

        public ICommand AddTeamCommand { get; }

        public async Task AddTeamAsync()
        {
            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var request = new TeamRequest
                {
                    Name = Name,
                    Description = Description
                };

                var response = await _routerService.CreateTeam(request);

                if (response != null && response.Status == "success")
                {
                    await _navigationService.NavigateToAsync<ListGroupsPage>();
                }
                else
                {
                    ErrorMessage = response?.Message ?? "Error al crear equipo";
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

        private bool CanAddTeam()
        {
            return !string.IsNullOrWhiteSpace(Name) 
                && !string.IsNullOrWhiteSpace(Description)
                && !IsBusy;
        }
    }
}
