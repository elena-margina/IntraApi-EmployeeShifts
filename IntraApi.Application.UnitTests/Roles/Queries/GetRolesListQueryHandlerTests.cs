using AutoMapper;
using IntraApi.Application.Contracts.Persistence;
using IntraApi.Application.Features.Roles.Queries.GetRolesList;
using IntraApi.Application.Profiles;
using IntraApi.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace IntraApi.Application.UnitTests.Roles.Queries
{
    public class GetRolesListQueryHandlerTests
    {
        private readonly Mock<IRoleRepository> _mockRoleRepository;
        private readonly IMapper _mapper;

        public GetRolesListQueryHandlerTests()
        {
            _mockRoleRepository = RepositoryMocks.GetRoleRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task GetRolesListTest()
        {
            var handler = new GetRolesListQueryHandler(_mapper, _mockRoleRepository.Object);

            var result = await handler.Handle(new GetRolesListQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<RoleListVm>>();

            result.Count.ShouldBe(3);
        }
    }
}
