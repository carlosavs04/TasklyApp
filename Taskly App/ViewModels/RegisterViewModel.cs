using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taskly_App.Models.Config;
using Taskly_App.Models.Requests;
using Taskly_App.Services;
using Taskly_App.Views.Login;

namespace Taskly_App.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly ApiRouterService _routerService;
        private readonly NavigationService _navigationService;
        private string _username = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;
        private string _passwordConfirmation = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isBusy = false;
        private Dictionary<string, List<string>>? _errors;

        public RegisterViewModel(ApiRouterService routerService, NavigationService navigationService)
        {
            _routerService = routerService;
            _navigationService = navigationService;
            RegisterCommand = new Command(async () => await RegisterAsync(), CanRegister);
        }

        public string Username
        {
            get => _username;
            set
            {
                if (SetProperty(ref _username, value))
                    ((Command)RegisterCommand).ChangeCanExecute();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (SetProperty(ref _email, value))
                    ((Command)RegisterCommand).ChangeCanExecute();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                    ((Command)RegisterCommand).ChangeCanExecute();
            }
        }

        public string PasswordConfirmation
        {
            get => _passwordConfirmation;
            set
            {
                if (SetProperty(ref _passwordConfirmation, value))
                    ((Command)RegisterCommand).ChangeCanExecute();
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
                    ((Command)RegisterCommand).ChangeCanExecute();
            }
        }

        public ICommand RegisterCommand { get; }

        public async Task RegisterAsync()
        {
            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                var request = new RegisterRequest
                {
                    Username = Username,
                    Email = Email,
                    Password = Password,
                    PasswordConfirmation = PasswordConfirmation
                };

                var response = await _routerService.Register(request);

                if (response != null && response.Status == "success")
                {
                    await _navigationService.NavigateToAsync<LoginPage>();
                }
                else
                {
                    ErrorMessage = response?.Message ?? "Error al registrar usuario";
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

        private bool CanRegister()
        {
            return !string.IsNullOrWhiteSpace(Username)
                && !string.IsNullOrWhiteSpace(Email)
                && !string.IsNullOrWhiteSpace(Password)
                && !string.IsNullOrWhiteSpace(PasswordConfirmation)
                && !IsBusy;
        }
    }
}
