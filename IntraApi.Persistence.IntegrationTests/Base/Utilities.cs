using IntraApi.Domain.Entities;
using IntraApi.Persistence;

namespace IntraApi.API.IntegrationTests.Base
{
    public class Utilities
    {
        public static void InitializeDbForTests(IntraApiDBContext context)
        {
            context.Roles.Add(new Role
            {
                Name = "Test Role 1",
                Description = "Test description 1",
                SeatsAvailable = 7,
                IsAvailable = true,
                IsPrimary = false,
                DModify = DateTime.Now,
                UserID = 1, 
                Version = 0
            });

            context.Roles.Add(new Role
            {
                Name = "Test Role 2",
                Description = "Test description 2",
                SeatsAvailable = 14,
                IsAvailable = true,
                IsPrimary = false,
                DModify = DateTime.Now,
                UserID = 1, 
                Version = 0
            });

            context.Roles.Add(new Role
            {
                Name = "Test Role 3",
                Description = "Test description 3",
                SeatsAvailable = 21,
                IsAvailable = true,
                IsPrimary = true,
                DModify = DateTime.Now,
                UserID = 1, 
                Version = 0
            });

            context.SaveChanges();
        }
    }
}
