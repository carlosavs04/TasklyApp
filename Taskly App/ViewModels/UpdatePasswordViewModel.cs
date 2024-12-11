using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taskly_App.Models.Config;
using Taskly_App.Models.Requests;
using Taskly_App.Services;

namespace Taskly_App.ViewModels
{
    public class UpdatePasswordViewModel : BaseViewModel
    {
        private ApiRouterService _routerService;
        private string _actualPassword = string.Empty;
        private string _password = string.Empty;
        private string _passwordConfirmation = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isBusy = false;
        private Dictionary<string, List<string>>? _errors;

        public UpdatePasswordViewModel(ApiRouterService routerService)
        {
            _routerService = routerService;
            UpdatePasswordCommand = new Command(async () => await UpdatePasswordAsync(), CanUpdatePassword);
        }

        public string ActualPassword
        {
            get => _actualPassword;
            set
            {
                if (SetProperty(ref _actualPassword, value))
                    ((Command)UpdatePasswordCommand).ChangeCanExecute();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                    ((Command)UpdatePasswordCommand).ChangeCanExecute();
            }
        }

        public string PasswordConfirmation
        {
            get => _passwordConfirmation;
            set
            {
                if (SetProperty(ref _passwordConfirmation, value))
                    ((Command)UpdatePasswordCommand).ChangeCanExecute();
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
                    ((Command)UpdatePasswordCommand).ChangeCanExecute();
            }
        }

        public ICommand UpdatePasswordCommand { get; }

        private async Task UpdatePasswordAsync()
        {
            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                var request = new UpdatePasswordRequest
                {
                    ActualPassword = ActualPassword,
                    Password = Password,
                    PasswordConfirmation = PasswordConfirmation
                };

                var response = await _routerService.UpdatePassword(request);
                if (response != null && response.Status == "success")
                {
                    if (Application.Current?.MainPage != null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Éxito", "Contraseña actualizada correctamente.", "OK");
                        await Application.Current.MainPage.Navigation.PopModalAsync();
                    }
                }
                else
                {
                    ErrorMessage = response?.Message ?? "Error al actualizar la contraseña.";
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

        private bool CanUpdatePassword()
        {
            return !IsBusy 
                && !string.IsNullOrWhiteSpace(ActualPassword) 
                && !string.IsNullOrWhiteSpace(Password) 
                && !string.IsNullOrWhiteSpace(PasswordConfirmation);
        }
    }
}
