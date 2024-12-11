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

        public async Task<ApiResponse<Team>?> GetTeam(int id)
        {
            return await _apiService.GetAsync<Team>($"teams/{id}", true);
        }

        public async Task<ApiResponse<object>?> UpdateTeam(int id, TeamRequest request)
        {
            return await _apiService.PutAsync<TeamRequest, object>($"teams/{id}", request, true);
        }

        public async Task<ApiResponse<object>?> UpdatePassword(UpdatePasswordRequest request)
        {
            return await _apiService.PutAsync<UpdatePasswordRequest, object>("users/update-password", request, true);
        }

        public async Task<ApiResponse<List<Note>>?> GetUserTasks(int id)
        {
            return await _apiService.GetAsync<List<Note>>($"tasks/{id}/user", true);
        }

        public async Task<ApiResponse<object>?> MarkTaskAsCompleted(int id)
        {
            return await _apiService.PutAsyncWithoutBody<object>($"tasks/complete/{id}", true);
        }

        public async Task<ApiResponse<object>?> UnmarkTaskAsCompleted(int id)
        {
            return await _apiService.PutAsyncWithoutBody<object>($"tasks/pending/{id}", true);
        }

        public async Task<ApiResponse<List<User>>?> GetUsersByTeam(int id)
        {
            return await _apiService.GetAsync<List<User>>($"teams/users/{id}", true);
        }

        public async Task<ApiResponse<object>?> RemoveUserFromTeam(int teamId, int userId)
        {
            return await _apiService.DeleteAsync<object>($"teams/remove-user/{teamId}/{userId}", true);
        }

        public async Task<ApiResponse<List<Note>>?> GetTeamTasks(int id)
        {
            return await _apiService.GetAsync<List<Note>>($"tasks/team/all/{id}", true);
        }

        public async Task<ApiResponse<Note>?> GetTask(int id)
        {
            return await _apiService.GetAsync<Note>($"tasks/{id}", true);
        }

        public async Task<ApiResponse<object>?> CreateTask(NoteRequest request)
        {
            return await _apiService.PostAsync<NoteRequest, object>("tasks/create", request, true);
        }

        public async Task<ApiResponse<object>?> UpdateTask(int id, NoteRequest request)
        {
            return await _apiService.PutAsync<NoteRequest, object>($"tasks/{id}", request, true);
        }

        public async Task<ApiResponse<object>?> RemoveTask(int id)
        {
            return await _apiService.DeleteAsync<object>($"tasks/{id}", true);
        }

        public async Task<ApiResponse<User>?> GetUser(int id)
        {
            return await _apiService.GetAsync<User>($"users/{id}", true);
        }
    }
}
