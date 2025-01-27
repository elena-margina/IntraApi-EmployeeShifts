using IntraApi.App.Models;

namespace IntraApi.App.Services
{
    public class EmployeeShiftService
    {
        private readonly HttpClient _httpClient;

        public EmployeeShiftService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("IntraApi");
        }

        public async Task<List<EmployeeShiftListVm>> GetEmployeeShiftsForCurrentWeekAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<GetEmployeesShiftsForCurrentWeek>("/api/EmployeeShift/GetEmployeesShiftsForCurrentWeek");

            return response?.Employees ?? new List<EmployeeShiftListVm>();
        }
        
        public async Task<ApiBaseResponse> AddShiftAsync(EmployeeShift shift)
        {
            var command = new AddEmployeeShiftCommand
            {
                EmployeeId = shift.EmployeeId,
                RoleId = shift.RoleId.GetValueOrDefault(),
                ShiftDate = shift.ShiftDate.GetValueOrDefault(),  
                StartTime = shift.StartTime.GetValueOrDefault(),
                EndTime = shift.EndTime.GetValueOrDefault()
            };

            var response = await _httpClient.PostAsJsonAsync("api/EmployeeShift/AddEmployeeShift", command);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiBaseResponse>();
            }
            else
            {
                return new ApiBaseResponse("Error adding shift") { Success = false };
            }
        }

        public async Task<ApiBaseResponse> EditShiftAsync(EmployeeShift shift)
        {
            var command = new UpdateEmployeeShiftCommand
            {
                EmployeeShiftId = shift.ShiftId,
                EmployeeRoleID = shift.EmployeeRoleId,
                EmployeeId = shift.EmployeeId.GetValueOrDefault(),
                RoleId = shift.RoleId.GetValueOrDefault(),
                ShiftDate = shift.ShiftDate.GetValueOrDefault(),  
                StartTime = shift.StartTime.GetValueOrDefault(),
                EndTime = shift.EndTime.GetValueOrDefault(),
            };

            var response = await _httpClient.PutAsJsonAsync("api/EmployeeShift/UpdateEmployeeShift", command);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiBaseResponse>();
            }
            else
            {
                return new ApiBaseResponse("Error updating shift") { Success = false };
            }
        }

        public async Task<ApiBaseResponse> DeleteShiftAsync(int employeeShiftId)
        {
            DeleteEmployeeShiftCommand command = new DeleteEmployeeShiftCommand { EmployeeShiftId = employeeShiftId };
            //var response = await _httpClient.PutAsJsonAsync("api/EmployeeShift/DeleteEmployeeShift", command);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("api/EmployeeShift/DeleteEmployeeShift", UriKind.Relative),
                Content = JsonContent.Create(command) // Add the command as JSON content
            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiBaseResponse>();
            }
            else
            {
                return new ApiBaseResponse("Failed to delete the shift.") { Success = false };
            }
        }


    }

}
