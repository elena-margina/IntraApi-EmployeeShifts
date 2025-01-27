using IntraApi.Application.Contracts.Persistence;
using IntraApi.Domain.Entities;

namespace IntraApi.Persistence.Repositories
{
    public class UserRepositoy : BaseRepository<User>, IUserRepository
    {
        public UserRepositoy(IntraApiDBContext dbContext) : base(dbContext)
        {
        }

    }
}
