using IntraApi.Application.Contracts.Persistence;
using IntraApi.Domain.Entities;

namespace IntraApi.Persistence.Repositories
{
    public class EmployeeRoleRepository : BaseRepository<EmployeeRole>, IEmployeeRoleRepository
    {
        public EmployeeRoleRepository(IntraApiDBContext dbContext) : base(dbContext)
        {
        }
    }
}
