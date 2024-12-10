using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taskly_App.Models.Config;
using Taskly_App.Services;
using Taskly_App.Views.Login;

namespace Taskly_App.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly ApiRouterService _routerService;
        private readonly NavigationService _navigationService;
        private string _errorMessage = string.Empty;
        private bool _isBusy = false;

        public SettingsViewModel(ApiRouterService routerService, NavigationService navigationService)
        {
            _routerService = routerService;
            _navigationService = navigationService;
            LogoutCommand = new Command(async () => await LogoutAsync());
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

        public ICommand LogoutCommand { get; }

        private async Task LogoutAsync()
        {
            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var response = await _routerService.Logout();
                if (response != null && response.Status == "success")
                {
                    await _navigationService.NavigateToAsync<LoginPage>();
                }
                else
                {
                    ErrorMessage = response?.Message ?? "Error al cerrar sesión";
                }
                
            }
            catch (ApiException ex)
            {
                ErrorMessage = ex.Message ?? "Error desconocido";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
