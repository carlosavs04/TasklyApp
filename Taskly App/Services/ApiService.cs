using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Taskly_App.Helpers;
using Taskly_App.Models.Config;

namespace Taskly_App.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ConfigurationService _configurationService;

        public ApiService(IOptions<ApiServiceConfig> options, ConfigurationService configurationService)
        {
            _configurationService = configurationService;

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(options.Value.BaseUrl)
            };
        }

        public async Task<ApiResponse<T>?> GetAsync<T>(string endpoint, bool requiresAuth = false)
        {
            ConfigureAuthorizationHeader(requiresAuth);
            var response = await _httpClient.GetAsync(endpoint);
            return await ProcessResponse<T>(response);
        }

        public async Task<ApiResponse<TResponse>?> PostAsync<TRequest, TResponse>(string endpoint, TRequest request, bool requiresAuth = false)
        { 
            ConfigureAuthorizationHeader(requiresAuth);
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, content);
            return await ProcessResponse<TResponse>(response);
        }

        public async Task<ApiResponse<T>?> PutAsync<T>(string endpoint, T request, bool requiresAuth = false)
        {
            ConfigureAuthorizationHeader(requiresAuth);
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(endpoint, content);
            return await ProcessResponse<T>(response);
        }

        public async Task<ApiResponse<T>?> DeleteAsync<T>(string endpoint, bool requiresAuth = false)
        {
            ConfigureAuthorizationHeader(requiresAuth);
            var response = await _httpClient.DeleteAsync(endpoint);
            return await ProcessResponse<T>(response);
        }

        private async Task<ApiResponse<T>?> ProcessResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<T>>(content, GetJsonOptions());

            if (response.IsSuccessStatusCode && apiResponse != null && apiResponse.Status == "success")
                return apiResponse;
            
            else
                throw new ApiException(apiResponse?.Message ?? "Error desconocido", apiResponse?.Errors);
        }

        private void ConfigureAuthorizationHeader(bool requiresAuth)
        {
            if (requiresAuth)
            {
                var token = _configurationService.GetAuthToken();
                if (!string.IsNullOrWhiteSpace(token))
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else 
                _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        private JsonSerializerOptions GetJsonOptions()
        {
            return new JsonSerializerOptions 
            { 
                PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
                PropertyNameCaseInsensitive = true 
            };
        }
    }
}
