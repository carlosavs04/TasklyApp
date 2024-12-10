using System.Windows.Input;
using Taskly_App.Models.Config;
using Taskly_App.Models.Requests;
using Taskly_App.Services;

namespace Taskly_App.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        private ApiRouterService _routerService;
        private string _email = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isBusy = false;
        private Dictionary<string, List<string>>? _errors;

        public ForgotPasswordViewModel(ApiRouterService routerService)
        {
            _routerService = routerService;
            SendEmailCommand = new Command(async () => await SendEmailAsync(), CanSendEmail);
        }

        public string Email
        {
            get => _email;
            set
            {
                if (SetProperty(ref _email, value))
                    ((Command)SendEmailCommand).ChangeCanExecute();
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
                    ((Command)SendEmailCommand).ChangeCanExecute();
            }
        }

        public ICommand SendEmailCommand { get; }

        private async Task SendEmailAsync()
        {
            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                var request = new EmailRequest { Email = Email };
                var response = await _routerService.RecoverPassword(request);

                if (response != null && response.Status == "success")
                {
                    if (Application.Current?.MainPage != null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Éxito", "Correo enviado. Revisa tu bandeja de entrada.", "OK");
                        await Application.Current.MainPage.Navigation.PopModalAsync();
                    }
                }
                else
                {
                    ErrorMessage = response?.Message ?? "Error al enviar el correo.";
                }
            }
            catch (ApiException ex)
            {
                _errors = ex.Errors as Dictionary<string, List<string>>;
                ErrorMessage = ex.Message ?? "Error desonocido";
            }
            finally
            {
                IsBusy = false;
            }   
        }

        private bool CanSendEmail()
        {
            return !IsBusy && !string.IsNullOrWhiteSpace(Email);
        }
    }
}
