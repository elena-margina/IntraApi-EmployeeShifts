using Microsoft.AspNetCore.Components;
using IntraApi.App.Models;
using IntraApi.App.Services;
using Microsoft.JSInterop;

namespace IntraApi.App.Pages
{
    public partial class EmployeeShifts
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; } = default!;        

        private string? errorMessage;
        private List<string>? validationErrors;
        private string currentWeekRange;

        private List<EmployeeShiftListVm> employeeShifts;
        private List<RoleListVm> roles;
        private readonly List<string> daysOfWeek = new() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        private bool isAddMode = false;
        private EmployeeShift newShift = new EmployeeShift();

        private bool isEditMode = false;
        private EmployeeShift editingShift = new();
        private EmployeeShift originalShift = new();

        private bool isDeleteMode = false;
        private int deletingShiftId;   

        protected override async Task OnInitializedAsync()
        {
            DateTime today = DateTime.Now;

            // Calculate the start and end dates of the current week (assuming the week starts on Monday)
            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfWeek = today.AddDays(-1 * diff).Date;
            DateTime endOfWeek = startOfWeek.AddDays(6).Date;

            currentWeekRange = $"{startOfWeek:dd.MM.yyyy} - {endOfWeek:dd.MM.yyyy}";
            employeeShifts = await EmployeeShiftService.GetEmployeeShiftsForCurrentWeekAsync();
            roles = await RoleService.GetAllRolesAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender || isAddMode || isEditMode || isDeleteMode)
            {
                if (isAddMode)
                {
                    await JSRuntime.InvokeVoidAsync("makeModalDraggable", "addShiftModal");
                }
                if (isEditMode)
                {
                    await JSRuntime.InvokeVoidAsync("makeModalDraggable", "editShiftModal");
                }
                if (isDeleteMode)
                {
                    await JSRuntime.InvokeVoidAsync("makeModalDraggable", "deleteShiftModal");
                }
            }
        }

        private void OpenAddShiftModal()
        {
            isAddMode = true;
            ResetShiftData();
        }

        private void ResetShiftData()
        {
            newShift = new EmployeeShift
            {
                EmployeeId = null,
                ShiftDate = null,
                RoleId = null,
                StartTime = null,
                EndTime = null
            };
        }

        private DateOnly GetDateForDay(string day)
        {
            // List of days of the week starting from Monday
            var daysOfWeek = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            // Get today's date
            var today = DateTime.Now.Date;

            // Find the current day of the week (adjust to Monday as the first day)
            int todayOffset = (int)today.DayOfWeek == 0 ? 6 : (int)today.DayOfWeek - 1;

            // Find the start of the current week (Monday)
            var startOfWeek = today.AddDays(-todayOffset);

            // Find the target day index
            var targetDayIndex = daysOfWeek.IndexOf(day);
            if (targetDayIndex == -1)
            {
                throw new ArgumentException($"Invalid day: {day}");
            }

            // Calculate the date for the target day
            var targetDate = startOfWeek.AddDays(targetDayIndex);

            return DateOnly.FromDateTime(targetDate);
        }


        private void AddShift(int employeeId, string day)
        {
            newShift.EmployeeId = employeeId;
            newShift.ShiftDate = GetDateForDay(day);

            isAddMode = true;
            StateHasChanged();
        }

        private void CloseAddShiftModal()
        {
            errorMessage = null;
            validationErrors = null;
            isAddMode = false;
            newShift = new EmployeeShift();
        }

        private async Task HandleAddShift()
        {
            try
            {
                var response = await EmployeeShiftService.AddShiftAsync(newShift);

                if (response.Success)
                {
                    isAddMode = false;
                    employeeShifts = await EmployeeShiftService.GetEmployeeShiftsForCurrentWeekAsync();
                    errorMessage = null;
                    validationErrors = null;
                }
                else
                {
                    errorMessage = response.Message;
                    validationErrors = response.ValidationErrors;
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"An unexpected error occurred: {ex.Message}";
                validationErrors = null;
            }
        }

        private void EditShift(EmployeeShift shift)
        {
            originalShift = new EmployeeShift
            {
                ShiftId = shift.ShiftId,
                EmployeeRoleId = shift.EmployeeRoleId,
                EmployeeId = shift.EmployeeId,
                RoleId = shift.RoleId,
                Role = shift.Role,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime
            };

            editingShift = shift;
            isEditMode = true;
            StateHasChanged();
        }

        private void CloseModal()
        {
            editingShift.EmployeeRoleId = originalShift.EmployeeRoleId;
            editingShift.EmployeeId = originalShift.EmployeeId;
            editingShift.RoleId = originalShift.RoleId;
            editingShift.StartTime = originalShift.StartTime;
            editingShift.EndTime = originalShift.EndTime;

            validationErrors = null;
            errorMessage = null;
            isEditMode = false;
        }

        private async Task HandleEditShift()
        {
            try
            {
                var response = await EmployeeShiftService.EditShiftAsync(editingShift);

                if (response.Success)
                {
                    isEditMode = false;
                    employeeShifts = await EmployeeShiftService.GetEmployeeShiftsForCurrentWeekAsync();
                    errorMessage = null;
                    validationErrors = null;
                }
                else
                {
                    errorMessage = response.Message;
                    validationErrors = response.ValidationErrors;
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"An unexpected error occurred: {ex.Message}";
                validationErrors = null;
            }
        }

        private void ConfirmDeleteShift()
        {
            errorMessage = null;
            validationErrors = null;

            deletingShiftId = editingShift.ShiftId;
            isDeleteMode = true;
            StateHasChanged();
        }

        private async Task HandleDeleteShift()
        {
            try
            {
                var response = await EmployeeShiftService.DeleteShiftAsync(deletingShiftId);

                if (response.Success)
                {
                    isDeleteMode = false;
                    isEditMode = false;
                    employeeShifts = await EmployeeShiftService.GetEmployeeShiftsForCurrentWeekAsync();
                    errorMessage = null;
                    validationErrors = null;
                }
                else
                {
                    errorMessage = response.Message;
                    validationErrors = response.ValidationErrors;
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"An unexpected error occurred: {ex.Message}";
                validationErrors = null;
            }
        }

        private void CloseDeleteModal()
        {
            validationErrors = null;
            errorMessage = null;
            isDeleteMode = false;
            deletingShiftId = 0;
        }
    }
}
