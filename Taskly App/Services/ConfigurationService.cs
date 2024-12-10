using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskly_App.Helpers;

namespace Taskly_App.Services
{
    public class ConfigurationService
    {
        private readonly UserPreferences _preferences;

        public ConfigurationService(UserPreferences userPreferences)
        {
            _preferences = userPreferences;
        }

        public void SetAuthToken(string authToken)
        {
            _preferences.SetPreference("auth_token", authToken);
        }

        public string GetAuthToken() 
        {
            return _preferences.GetPreference("auth_token", "auth_token");
        }

        public void SetUserId(int userId)
        {
            _preferences.SetPreference("user_id", userId);
        }

        public int GetUserId()
        {
            return _preferences.GetPreference("user_id", 0);
        }

        public void SetSelectedTeamId(int teamId)
        {
            _preferences.SetPreference("selected_team_id", teamId);
        }

        public int GetSelectedTeamId()
        {
            return _preferences.GetPreference("selected_team_id", 0);
        }

        public void SetOwnerTeamId(int ownerId)
        {
            _preferences.SetPreference("owner_team_id", ownerId);
        }

        public int GetOwnerTeamId()
        {
            return _preferences.GetPreference("owner_team_id", 0);
        }
    }
}
