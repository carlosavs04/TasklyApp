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
    public class LoginViewModel : BaseViewModel
    {
        private readonly ApiRouterService _routerService;
        private readonly NavigationService _navigationService;
        private string _userOrEmail = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isBusy = false;
        private Dictionary<string, List<string>>? _errors;

        public LoginViewModel(ApiRouterService routerService, NavigationService navigationService)
        {
            _routerService = routerService;
            _navigationService = navigationService;
            LoginCommand = new Command(async () => await LoginAsync(), CanLogin);
        }

        public string UserOrEmail
        {
            get => _userOrEmail;
            set
            {
                if (SetProperty(ref _userOrEmail, value))
                    ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                    ((Command)LoginCommand).ChangeCanExecute();
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
                    ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        public ICommand LoginCommand { get; }

        public async Task LoginAsync()
        {
            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                var isEmail = UserOrEmail.Contains("@");
                var request = new LoginRequest
                {
                    Email = isEmail ? UserOrEmail : null,
                    Username = isEmail? null: UserOrEmail,
                    Password = Password
                };

                var response = await _routerService.Login(request);

                if (response != null && response.Status == "success")
                {
                    await _navigationService.NavigateToAsync<ListGroupsPage>();
                }
                else
                {
                    ErrorMessage = response?.Message ?? "Error al iniciar sesión";
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

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(UserOrEmail)
                && !string.IsNullOrWhiteSpace(Password)
                && !IsBusy;
        }
    }
}
