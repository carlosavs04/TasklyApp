using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Taskly_App.Models.Config;
using Taskly_App.Models.Requests;
using Taskly_App.Services;

namespace Taskly_App.ViewModels
{
    public class TeamDetailsViewModel : BaseViewModel
    {
        private readonly ApiRouterService _routerService;
        private readonly ConfigurationService _configurationService;
        private int _teamId;
        private Models.Team? _team;
        private string _originalName = string.Empty;
        private string _originalDescription = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isBusy = false;
        private bool _isEditing = false;
        private bool _isGroupOwner = false;
        private Dictionary<string, List<string>>? _errors;

        public TeamDetailsViewModel(ApiRouterService routerService, ConfigurationService configurationService)
        {
            _routerService = routerService;
            _configurationService = configurationService;
            _teamId = configurationService.GetSelectedTeamId();
            LoadTeamCommand = new Command(async () => await LoadTeamAsync());
            UpdateTeamCommand = new Command(async () => await UpdateTeamAsync(), CanUpdateTeam);
            EditTeamCommand = new Command(OnEdit);
            CancelEditCommand = new Command(OnCancel);
            IsOwner();
        }

        public Models.Team? Team
        {
            get => _team;
            set
            {
                if (SetProperty(ref _team, value))
                {
                    OnPropertyChanged(nameof(Name));
                    OnPropertyChanged(nameof(Description));
                    OnPropertyChanged(nameof(Code));
                }
            }
        }

        public string Name
        {
            get => Team?.Name ?? string.Empty;
            set
            {
                if (Team != null)
                {
                    Team.Name = value;
                    OnPropertyChanged(nameof(Name));
                    ((Command)UpdateTeamCommand).ChangeCanExecute();
                }
            }
        }

        public string Description
        {
            get => Team?.Description ?? string.Empty;
            set
            {
                if (Team != null)
                {
                    Team.Description = value;
                    OnPropertyChanged(nameof(Description));
                    ((Command)UpdateTeamCommand).ChangeCanExecute();
                }
            }
        }

        public string Code
        {
            get => Team?.Code ?? string.Empty;
            set
            {
                if (Team != null)
                {
                    Team.Code = value;
                    OnPropertyChanged(nameof(Code));
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
                    ((Command)UpdateTeamCommand).ChangeCanExecute();
            }
        }

        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                if (SetProperty(ref _isEditing, value))
                    UpdateShowEditButton();
            }
        }

        public bool IsGroupOwner
        {
            get => _isGroupOwner;
            set
            {
                if (SetProperty(ref _isGroupOwner, value))
                    UpdateShowEditButton();
            }
        }

        public bool ShowEditButton => !IsEditing && IsGroupOwner;

        public ICommand LoadTeamCommand { get; }
        public ICommand UpdateTeamCommand { get; }
        public ICommand EditTeamCommand { get; }
        public ICommand CancelEditCommand { get; }

        public async Task LoadTeamAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var response = await _routerService.GetTeam(_teamId);

                if (response != null && response.Status == "success" && response.Data != null)
                {
                    Team = response.Data;
                }
                else
                {
                    ErrorMessage = "No se pudo cargar la información del equipo";
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

        public async Task UpdateTeamAsync()
        {
            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                if (Team != null)
                {
                    var request = new TeamRequest
                    {
                        Name = Team.Name,
                        Description = Team.Description
                    };

                    var response = await _routerService.UpdateTeam(_teamId, request);

                    if (response != null && response.Status == "success")
                    {
                        IsEditing = false;
                    }
                    else
                    {
                        ErrorMessage = response?.Message ?? "Error al actualizar equipo";
                    }
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

        private void OnEdit()
        {
            if (Team != null)
            {
                _originalName = Team.Name;
                _originalDescription = Team.Description;
            }
            IsEditing = true;
        }

        private void OnCancel()
        {
            if (Team != null)
            {
                Team.Name = _originalName;
                Team.Description = _originalDescription;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Description));
            }
            IsEditing = false;
        }

        private bool CanUpdateTeam()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Description)
                && !IsBusy;
        }

        private void IsOwner()
        {
            IsGroupOwner = _configurationService.GetOwnerTeamId() == _configurationService.GetUserId();
        }

        private void UpdateShowEditButton()
        {
            OnPropertyChanged(nameof(ShowEditButton));
        }
    }
}
