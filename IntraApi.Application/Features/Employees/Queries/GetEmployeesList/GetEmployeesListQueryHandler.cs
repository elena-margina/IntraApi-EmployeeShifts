using IntraApi.Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using IntraApi.Application.Features.Employees.Queries.GetEmployeesList;

namespace IntraApi.Application.Features.Students.Queries.GetStudentsList
{
    public class GetStudentsListQueryHandler : IRequestHandler<GetEmployeesListQuery, List<EmployeeListVm>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetStudentsListQueryHandler(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeListVm>> Handle(GetEmployeesListQuery request, CancellationToken cancellationToken)
        {
            var allEmployees = (await _employeeRepository.ListAllAsync()).OrderBy(x => x.FullName);
            
            return _mapper.Map<List<EmployeeListVm>>(allEmployees);
        }
    }
}
