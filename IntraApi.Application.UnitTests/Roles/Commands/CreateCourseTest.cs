

using AutoMapper;
using IntraApi.Application.Contracts.Persistence;
using IntraApi.Application.Profiles;
using IntraApi.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace IntraApi.Application.UnitTests.Courses.Commands
{
    public class CreateCourseTest
    {
        private readonly Mock<IRoleRepository> _mockRoleRepository;
        private readonly IMapper _mapper;

        public CreateCourseTest()
        {
            _mockRoleRepository = RepositoryMocks.GetRoleRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        //[Fact]
        //public async Task Handle_ValidCategory_AddedToCoursesRepo()
        //{
        //    var handler = new CreateRoleCommandHandler(_mapper, _mockCourseRepository.Object);
        //    var roleAdd = new CreateRoleCommand()
        //    {
        //        Name = "Test Role 1",
        //        Description = "Test description 1",
        //        SeatsAvailable = 7,
        //        IsAvailable = true,
        //        IsPrimary = false,
        //        UserID = 1,
        //    };

        //    await handler.Handle(roleAdd, CancellationToken.None);

        //    var allRoless = await _mockRoleRepository.Object.ListAllAsync();
        //    allRoless.Count.ShouldBe(4);
        //}
    }
}
