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
    }
}
