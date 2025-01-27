using IntraApi.App.Models;

namespace IntraApi.App.Services
{
    public class RoleService
    {
        private readonly HttpClient _httpClient;

        public RoleService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("IntraApi");
        }

        public async Task<List<RoleListVm>> GetAllRolesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<RoleListVm>>("api/Role/GetAllRole");
            return response ?? new List<RoleListVm>();
        }
    }
}
