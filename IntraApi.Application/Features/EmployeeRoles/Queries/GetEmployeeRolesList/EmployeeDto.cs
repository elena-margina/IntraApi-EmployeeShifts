﻿namespace IntraApi.Application.Features.EmployeeRoles.Queries.GetEmployeeRoleList
{
    public class EmployeeDto
    {
        public int ID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
