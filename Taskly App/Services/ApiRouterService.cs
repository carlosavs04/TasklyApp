using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskly_App.Models;
using Taskly_App.Models.Config;
using Taskly_App.Models.Requests;

namespace Taskly_App.Services
{
    public class ApiRouterService
    {
        private readonly ApiService _apiService;
        private readonly ConfigurationService _configurationService;

        public ApiRouterService(ApiService apiService, ConfigurationService configurationService)
        {
            _apiService = apiService;
            _configurationService = configurationService;
        }

        public async Task<ApiResponse<object>?> Register(RegisterRequest request)
        {
            return await _apiService.PostAsync<RegisterRequest, object>("auth/register", request);
        }

        public async Task<ApiResponse<User>?> Login(LoginRequest request)
        {
            var response = await _apiService.PostAsync<LoginRequest, User>("auth/login", request);

            if (response != null && response.Token != null && response.Data != null)
            {
                _configurationService.SetAuthToken(response.Token);
                _configurationService.SetUserId(response.Data.Id);
            }
                
            return response;
        }

        public async Task<ApiResponse<object>?> RecoverPassword(EmailRequest request)
        {
            return await _apiService.PostAsync<EmailRequest, object>("auth/recover-password", request);
        }

        public async Task<ApiResponse<object>?> Logout()
        {
            var response = await _apiService.DeleteAsync<object>("auth/logout", true);
            _configurationService.SetAuthToken("auth_token");
            return response;
        }

        public async Task<ApiResponse<List<Team>>?> GetTeamsByUser()
        {
            return await _apiService.GetAsync<List<Team>>("teams", true);
        }

        public async Task<ApiResponse<object>?> CreateTeam(TeamRequest request)
        {
            return await _apiService.PostAsync<TeamRequest, object>("teams/create", request, true);
        }

        public async Task<ApiResponse<object>?> JoinTeamByCode(CodeRequest request)
        {
            return await _apiService.PostAsync<CodeRequest, object>("teams/join", request, true);
        }

        public async Task<ApiResponse<object>?> UsersList()
        {
            return await _apiService.GetAsync<object>("users", true);
        }
    }
}
