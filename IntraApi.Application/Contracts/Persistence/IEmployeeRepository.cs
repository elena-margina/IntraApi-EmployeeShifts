using IntraApi.Domain.Entities;

namespace IntraApi.Application.Contracts.Persistence
{
    public  interface IEmployeeRepository : IAsyncRepository<Employee>
    {
    }
}
