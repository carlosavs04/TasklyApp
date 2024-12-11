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
    public class TeamMembersViewModel : BaseViewModel
    {
        private readonly ApiRouterService _routerService;
        private readonly ConfigurationService _configurationService;
        private int _teamId;
        private string _errorMessage = string.Empty;
        private bool _isBusy = false;
        private bool _isGroupOwner = false;
        private ObservableCollection<User> _members = new ObservableCollection<User>();

        public TeamMembersViewModel(ApiRouterService routerService, ConfigurationService configurationService)
        {
            _routerService = routerService;
            _configurationService = configurationService;
            _teamId = configurationService.GetSelectedTeamId();
            LoadMembersCommand = new Command(async () => await LoadMembersAsync());
            RemoveMemberCommand = new Command<int>(async (id) => await RemoveMemberAsync(id));
            IsOwner();
        }

        public ObservableCollection<User> Members
        {
            get => _members;
            set => SetProperty(ref _members, value);
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

        public bool IsGroupOwner
        {
            get => _isGroupOwner;
            set => SetProperty(ref _isGroupOwner, value);
        }

        public ICommand LoadMembersCommand { get; }
        public ICommand RemoveMemberCommand { get; }

        private async Task LoadMembersAsync()
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
                    var userId = _configurationService.GetUserId();
                    var userList = response.Data as List<User>;
                    if (userList != null)
                    {
                        foreach (var user in userList)
                        {
                            user.CanBeRemoved = IsGroupOwner && user.Id != userId;
                        }
                        Members = new ObservableCollection<User>(userList);
                    }
                }
                else
                {
                    ErrorMessage = "No se pudo cargar la información del equipo.";
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

        private async Task RemoveMemberAsync(int id)
        {
            if (IsBusy)
                return;

            bool confirm = false;

            if (Application.Current?.MainPage != null)
                confirm = await Application.Current.MainPage.DisplayAlert("Eliminar usuario", "¿Estás seguro de eliminar al usuario del equipo?", "Sí", "No");

            if (!confirm)
                return;

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                var response = await _routerService.RemoveUserFromTeam(_teamId, id);

                if (response != null && response.Status == "success")
                {
                    var userToRemove = Members.FirstOrDefault(u => u.Id == id);
                    if (userToRemove != null)
                        Members.Remove(userToRemove);
                }
                else
                {
                    ErrorMessage = "No se pudo eliminar al usuario del equipo.";
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

        private void IsOwner()
        {
            IsGroupOwner = _configurationService.GetUserId() == _configurationService.GetOwnerTeamId();
        }
    }
}
