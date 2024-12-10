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
    public class JoinTeamViewModel : BaseViewModel
    {
        private readonly ApiRouterService _routerService;
        private readonly NavigationService _navigationService;
        private string _code = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isBusy = false;
        private Dictionary<string, List<string>>? _errors;

        public JoinTeamViewModel(ApiRouterService routerService, NavigationService navigationService)
        {
            _routerService = routerService;
            _navigationService = navigationService;
            JoinTeamCommand = new Command(async () => await JoinTeamAsync(), CanJoinTeam);
        }

        public string Code
        {
            get => _code;
            set
            {
                if (SetProperty(ref _code, value))
                    ((Command)JoinTeamCommand).ChangeCanExecute();
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
                    ((Command)JoinTeamCommand).ChangeCanExecute();
            }
        }

        public ICommand JoinTeamCommand { get; }

        private bool CanJoinTeam() => !string.IsNullOrWhiteSpace(_code) && !_isBusy;

        private async Task JoinTeamAsync()
        {
            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var request = new CodeRequest { Code = Code };
                var response = await _routerService.JoinTeamByCode(request);
                
                if (response != null && response.Status == "success")
                {
                    await _navigationService.NavigateToAsync<ListGroupsPage>();
                }
                else
                {
                    ErrorMessage = response?.Message ?? "Error al unirse al equipo";
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
    }
}
