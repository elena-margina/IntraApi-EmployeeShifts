using Moq;
using Shouldly;
using IntraApi.Application.Contracts;
using Microsoft.EntityFrameworkCore;
using IntraApi.Domain.Entities;

namespace IntraApi.Persistence.IntegrationTests
{
    public class IntraApiDBContextTest
    {
        private readonly IntraApiDBContext _intraApiDbContext;
        private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
        private readonly int _loggedInUserId;

        public IntraApiDBContextTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<IntraApiDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _loggedInUserId = 1; 
            _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            _loggedInUserServiceMock.Setup(m => m.UserId).Returns(_loggedInUserId);

            _intraApiDbContext = new IntraApiDBContext(dbContextOptions, _loggedInUserServiceMock.Object);
        }

        [Fact]
        public async void Save_SetCreatedByProperty()
        {
            var Role = new Role()
            {
                Name = "Test Role 1",
                Description = "Test description 1",
                SeatsAvailable = 7,
                IsAvailable = true,
                IsPrimary = false,
                UserID = 1,
            };

            _intraApiDbContext.Roles.Add(Role);
            await _intraApiDbContext.SaveChangesAsync();

            Role.UserID.ShouldBe(_loggedInUserId);
        }
    }
}
