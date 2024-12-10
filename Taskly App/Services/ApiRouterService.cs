﻿using System;
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
            return await _apiService.PostAsync<object, object>("auth/register", request);
        }

        public async Task<ApiResponse<object>?> Login(LoginRequest request)
        {
            var response = await _apiService.PostAsync<object, object>("auth/login", request);

            if (response != null && response.Token != null) 
                _configurationService.SetAuthToken(response.Token);

            return response;
        }

        public async Task<ApiResponse<object>?> RecoverPassword(EmailRequest request)
        {
            return await _apiService.PostAsync<object, object>("auth/recover-password", request);
        }

        public async Task<ApiResponse<object>?> Logout()
        {
            _configurationService.SetAuthToken("auth_token");
            return await _apiService.DeleteAsync<object>("auth/logout", true);
        }

        public async Task<ApiResponse<List<Team>>?> GetTeamsByUser()
        {
            return await _apiService.GetAsync<List<Team>>("teams", true);
        }

        public async Task<ApiResponse<object>?> UsersList()
        {
            return await _apiService.GetAsync<object>("users", true);
        }
    }
}