using EmptyFiles;
using IntraApi.Application.Contracts.Persistence;
using IntraApi.Domain.Entities;
using Moq;

namespace IntraApi.Application.UnitTests.Mocks
{
    public static class RepositoryMocks
    {
        public static Mock<IRoleRepository> GetRoleRepository()
        {
            int testRole1ID = 1;
            int testRole2ID = 2;
            int testRole3ID = 3;

            var roles = new List<Role>
            {
                new Role
                {
                    ID = testRole1ID,
                    Name = "Test Role 1",
                    Description = "Test description 1",
                    SeatsAvailable = 7,
                    IsAvailable = true,
                    IsPrimary = false,
                    UserID = 1,
                    DModify = DateTime.Now,
                    Version = 1
                },
                new Role
                {
                    ID = testRole2ID,
                    Name = "Test Role 2",
                    Description = "Test description 2",
                    SeatsAvailable = 14,
                    IsAvailable = true,
                    IsPrimary = false,
                    UserID = 2,
                    DModify = DateTime.Now,
                    Version = 1
                },
                new Role
                {
                    ID = testRole3ID,
                    Name = "Test Role 3",
                    Description = "Test description 3",
                    SeatsAvailable = 21,
                    IsAvailable = true,
                    IsPrimary = false,
                    UserID = 3,
                    DModify = DateTime.Now,
                    Version = 1
                }
            };

            var mockRoleRepository = new Mock<IRoleRepository>();

            // Setup for listing all roles
            mockRoleRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(roles);

            // Setup for adding a new role
            mockRoleRepository.Setup(repo => repo.AddAsync(It.IsAny<Role>())).ReturnsAsync(
                (Role role) =>
                {
                    roles.Add(role);
                    return role;
                });

            // Setup for retrieving a role by ID
            mockRoleRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) => roles.Find(r => r.ID == id));

            return mockRoleRepository;
        }
    }
}
