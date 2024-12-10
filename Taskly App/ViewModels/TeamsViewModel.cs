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
    public class TeamsViewModel : BaseViewModel
    {
        private readonly ApiRouterService _routerService;
        private string _errorMessage = string.Empty;
        private bool _isBusy = false;
        public ObservableCollection<Team> _teams = new ObservableCollection<Team>();

        public TeamsViewModel(ApiRouterService routerService)
        {
            _routerService = routerService;
            LoadTeamsCommand = new Command(async () => await LoadTeamsAsync());
        }

        public ObservableCollection<Team> Teams
        {
            get => _teams;
            set => SetProperty(ref _teams, value);
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

        public ICommand LoadTeamsCommand { get; }

        private async Task LoadTeamsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                var response = await _routerService.GetTeamsByUser();
                if (response != null && response.Status == "success" && response.Data != null && response.Data.Count > 0)
                {
                    var teamsList = response.Data as List<Team>;
                    if (teamsList != null)
                        Teams = new ObservableCollection<Team>(teamsList);
                }
                else
                {
                    ErrorMessage = "Parece que aún no perteneces a ningún equipo";
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
    }
}
