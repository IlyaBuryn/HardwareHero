using HardwareHero.Shared.Repositories.EF;
using HardwareHero.Shared.Tests.Repositories;
using HardwareHero.Shared.Tests;
using HardwareHero.Shared.Models;
using HardwareHero.Shared.Exceptions;
using HardwareHero.Filter.RequestsModels;
using HardwareHero.Filter.Operations;

public class EFCollectionRepositoryAsync_IntegrationTests
{
    private readonly IFixture _fixture;
    private readonly EFQueryRepositoryAsync<TestEntity> _repository;
    private TestDbContext _context;

    public EFCollectionRepositoryAsync_IntegrationTests()
    {
        _fixture = new Fixture();
        _context = TestDbContext.GetInMemoryDbContext();
        _repository = new EFQueryRepositoryAsync<TestEntity>(_context);
    }

    public class TestPaginable : FilterRequestDomain<TestEntity>, IPaginable
    {
        public TestPaginable(uint pn, uint ps)
        {
            PageNumber = pn;
            PageSize = ps;
        }

        public uint PageNumber { get; init; }
        public uint PageSize { get; init; }
    }

    [Fact]
    public async Task GetPageAsync_ReturnQuery_WhenItSuccessfully()
    {
        // Arrange
        int count = 30;
        await SetupTestEntitiesAsync(count);
        var resultStamp = new List<TestEntity>();

        for (uint i = 1; i < (count / 10) + 2; i++)
        {

            var paginationSettings = new TestPaginable(i, 10);

            // Act
            var result = (await _repository.FindPagedAsync(null, paginationSettings)).Values;

            // Assert
            if (i == (count / 10) + 1)
            {
                result.Should().NotHaveCount(10);
            }
            else
            {
                result.Should().NotBeNullOrEmpty();
                result.Should().NotBeEquivalentTo(resultStamp);
                result.Should().HaveCount(10);
            }
            resultStamp = (List<TestEntity>)result;
        }
    }

    [Fact]
    public async Task GetPageAsync_ReturnQueryWithoutEagerLoading_WhenItSuccessfully()
    {
        // Arrange
        int count = 30;
        await SetupTestEntitiesAsync(count);
        var props = new TestPaginable(1, 10);

        // Act
        var result = (await _repository.FindPagedAsync(null, props)).Values;

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(10);
        result.First().AnotherTestEntity.Should().BeNull();
    }

    [Fact]
    public async Task GetPageAsync_ThrowException_WhenNoPaginationProps()
    {
        // Arrange
        int count = 30;
        await SetupTestEntitiesAsync(count);

        // Act
        Func<Task> act = async () => await _repository.FindPagedAsync(null, null);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task GetPageAsync_ThrowException_WhenPaginationPropsIsWrong()
    {
        // Arrange
        int count = 30;
        await SetupTestEntitiesAsync(count);
        var props = new TestPaginable(0, 0);

        // Act
        Func<Task> act = async () => await _repository.FindPagedAsync(null, props);

        // Assert
        await act.Should().ThrowAsync<PageOptionsValidationException>();
    }

    //public async Task<PageResponse<MapType?>> GetMappedPageAsync<MapType>(IQueryable<T?>? query, [NotNull] PaginationInfo paginationInfo, [NotNull] IMapper mapper)
    //{
    //    if (query == null)
    //    {
    //        query = Enumerable.Empty<T?>().AsQueryable();
    //    }

    //    var items = await GetPageAsync(query, paginationInfo);
    //    var pageTotal = await GetTotalPageCountAsync(query, paginationInfo);

    //    var pageItems = new List<MapType?>();
    //    if (mapper != null)
    //    {
    //        pageItems = mapper.Map<List<MapType?>>(items);
    //    }

    //    return new PageResponse<MapType?>
    //    {
    //        Items = pageItems,
    //        TotalPages = pageTotal,
    //        CurrentPaginationInfo = paginationInfo,
    //    };
    //}

    private async Task SetupTestEntitiesAsync(int count)
    {
        var items = _fixture.CreateMany<TestEntity>(count);
        foreach (var item in items)
        {
            await _repository.CreateEntityAsync(item);
        }
    }
}