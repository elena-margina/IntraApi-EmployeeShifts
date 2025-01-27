using IntraApi.Application.Contracts.Persistence;
using IntraApi.Application.Extensions;
using IntraApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntraApi.Persistence.Repositories
{
    public class EmployeeShiftRepository : BaseRepository<EmployeeShift>, IEmployeeShiftRepository
    {
        public EmployeeShiftRepository(IntraApiDBContext dbContext) : base(dbContext)
        {
        }
    }
}
