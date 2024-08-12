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
    private readonly EFCollectionRepositoryAsync<TestEntity> _repository;
    private TestDbContext _context;

    public EFCollectionRepositoryAsync_IntegrationTests()
    {
        _fixture = new Fixture();
        _context = TestDbContext.GetInMemoryDbContext();
        _repository = new EFCollectionRepositoryAsync<TestEntity>(_context);
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
        var query = await _repository.GetManyEntitiesAsync();
        var resultStamp = new List<TestEntity>();

        for (uint i = 1; i < (count / 10) + 2; i++)
        {

            var paginationSettings = new TestPaginable(i, 10);

            // Act
            var result = await _repository.GetPageAsync(query, paginationSettings);

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
        var query = await _repository.GetManyEntitiesAsync();
        var props = new TestPaginable(1, 10);

        // Act
        var result = await _repository.GetPageAsync(query, props);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().HaveCount(10);
        result.First().AnotherTestEntity.Should().BeNull();
    }

    [Fact]
    public async Task GetPageAsync_ReturnEmptyQuery_WhenNoEntryQuery()
    {
        // Arrange
        var props = new TestPaginable(1, 10);

        // Act
        var result = await _repository.GetPageAsync(null, props);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(0);       
    }

    [Fact]
    public async Task GetPageAsync_ThrowException_WhenNoPaginationProps()
    {
        // Arrange
        int count = 30;
        await SetupTestEntitiesAsync(count);
        var query = await _repository.GetManyEntitiesAsync();

        // Act
        Func<Task> act = async () => await _repository.GetPageAsync(query, null);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task GetPageAsync_ThrowException_WhenPaginationPropsIsWrong()
    {
        // Arrange
        int count = 30;
        await SetupTestEntitiesAsync(count);
        var query = await _repository.GetManyEntitiesAsync();
        var props = new TestPaginable(0, 0);

        // Act
        Func<Task> act = async () => await _repository.GetPageAsync(query, props);

        // Assert
        await act.Should().ThrowAsync<PageOptionsValidationException>();
    }

    [Fact]
    public async Task GetTotalPageCountAsync_ReturnCorrectCount_WhenItSuccessfully()
    {
        // Arrange
        int count = 37;
        await SetupTestEntitiesAsync(count);
        var query = await _repository.GetManyEntitiesAsync();
        var props = new TestPaginable(1, 10);
        var expected = count % props.PageSize == 0 ?
            count / props.PageSize : 
            (count / props.PageSize) + 1;

        // Act
        var result = await _repository.GetTotalPageCountAsync(query, props);

        // Assert
        result.Should().Be((int)expected);
    }

    [Fact]
    public async Task GetTotalPageCountAsync_ReturnZero_WhenEntryQueryIsNull()
    {
        // Arrange
        var props = new TestPaginable(1, 10);

        // Act
        var result = await _repository.GetTotalPageCountAsync(null, props);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public async Task GetTotalPageCountAsync_ThrowException_WhenNoPaginationProps()
    {
        // Arrange
        int count = 30;
        await SetupTestEntitiesAsync(count);
        var query = await _repository.GetManyEntitiesAsync();

        // Act
        Func<Task> act = async () => await _repository.GetTotalPageCountAsync(query, null);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task GetTotalPageCountAsync_ThrowException_WhenPaginationPropsIsWrong()
    {
        // Arrange
        int count = 30;
        await SetupTestEntitiesAsync(count);
        var query = await _repository.GetManyEntitiesAsync();
        var props = new TestPaginable(0, 0);

        // Act
        Func<Task> act = async () => await _repository.GetTotalPageCountAsync(query, props);

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