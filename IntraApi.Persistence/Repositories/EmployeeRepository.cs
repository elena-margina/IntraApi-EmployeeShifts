using IntraApi.Application.Contracts.Persistence;
using IntraApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntraApi.Persistence.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IntraApiDBContext dbContext) : base(dbContext)
        {
        }
    }
}
