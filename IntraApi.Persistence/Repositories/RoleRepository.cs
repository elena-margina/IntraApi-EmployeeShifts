using IntraApi.Application.Contracts.Persistence;
using IntraApi.Domain.Entities;

namespace IntraApi.Persistence.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IntraApiDBContext dbContext) : base(dbContext)
        {
        }
    }
}
