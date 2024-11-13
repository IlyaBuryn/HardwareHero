using AutoFixture.Xunit2;
using HardwareHero.Shared.Repositories;
using HardwareHero.Shared.Repositories.EF;
using Microsoft.EntityFrameworkCore;

namespace HardwareHero.Shared.Tests.Repositories.EF
{
    public class EFCrudRepositoryAsync_IntegrationTests
    {
        private readonly IFixture _fixture;
        private readonly EFBaseRepositoryAsync<TestEntity> _repository;
        private readonly EFBaseRepositoryAsync<AnotherTestEntity> _tmpRepository;
        private TestDbContext _context;

        public EFCrudRepositoryAsync_IntegrationTests()
        {
            _fixture = new Fixture();
            _context = TestDbContext.GetInMemoryDbContext();
            _repository = new EFBaseRepositoryAsync<TestEntity>(_context);
            _tmpRepository = new EFBaseRepositoryAsync<AnotherTestEntity>(_context);
        }

        [Theory, AutoData]
        public async Task CreateEntityAsync_ShouldAddEntityToDb_ReturnEntityIdIfSuccessfully(TestEntity entity)
        {
            // Act
            var creationResult = await _repository.CreateEntityAsync(entity);
            var existingResult = await _context.Set<TestEntity>().FindAsync(entity.Id);

            // Assert creationResult
            creationResult.Should().Be(entity.Id);

            // Assert existingResult
            existingResult.Should().NotBeNull();
            existingResult.Should().BeEquivalentTo(entity);
        }

        [Theory, AutoData]
        public async Task CreateEntityAsync_ShouldAddRelatedEntities_ReturnEntityIdIfSuccessfully(TestEntity entity)
        {
            // Act
            var creationResult = await _repository.CreateEntityAsync(entity);
            var existingOriginalResult = await _context.Set<TestEntity>().FindAsync(entity.Id);
            var existingRelatedOneResult = await _context.Set<AnotherTestEntity>().FindAsync(entity.AnotherTestEntity.Id);
            var existingRelatedDeepResult = await _context.Set<DeepRelatedTestEntity>()
                .FindAsync(entity.AnotherTestEntity.DeepRelatedTestEntity.Id);

            // Assert
            creationResult.Should().Be(entity.Id);
            existingOriginalResult.Should().NotBeNull();
            existingRelatedOneResult.Should().NotBeNull();
            existingRelatedDeepResult.Should().NotBeNull();
        }

        [Theory, AutoData]
        public async Task CreateEntityAsync_ShouldAddRelatedEntities_WhenItsAlreadyExist(TestEntity entity, AnotherTestEntity anotherTestEntity)
        {
            // Arrange
            var anotherTestEntityCreationResult = await _tmpRepository.CreateEntityAsync(anotherTestEntity);
            entity.AnotherTestEntity = anotherTestEntity;

            // Act
            var creationResult = await _repository.CreateEntityAsync(entity);
            var existingOriginalResult = await _context.Set<TestEntity>().FindAsync(entity.Id);
            var existingRelatedOneResult = await _context.Set<AnotherTestEntity>().FindAsync(entity.AnotherTestEntity.Id);

            // Assert
            anotherTestEntityCreationResult.Should().Be(anotherTestEntity.Id);
            creationResult.Should().Be(entity.Id);
            existingOriginalResult.Should().NotBeNull();
            existingRelatedOneResult.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateEntityAsync_ShouldThrowException_WhenEntityIsNull()
        {
            // Act
            Func<Task> act = async () => await _repository.CreateEntityAsync(null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory, AutoData]
        public async Task CreateEntityAsync_ShouldReturnEmptyId_WhenItAlreadyExist(TestEntity entity)
        {
            // Act
            await _repository.CreateEntityAsync(entity);
            var creationResult = await _repository.CreateEntityAsync(entity);

            // Assert
            creationResult.Should().Be(Guid.Empty);
        }

        [Theory, AutoData]
        public async Task CreateEntityAsync_ShouldThrowException_WhenRelatedEntityAlreadyExist(TestEntity entity, AnotherTestEntity anotherTestEntity)
        {
            // Arrange
            var anotherTestEntityCreationResult = await _tmpRepository.CreateEntityAsync(anotherTestEntity);
            var updated = _fixture.Create<AnotherTestEntity>();
            updated.Id = anotherTestEntity.Id;
            entity.AnotherTestEntity = updated;

            // Act
            Func<Task> act = async () => await _repository.CreateEntityAsync(entity);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Theory, AutoData]
        public async Task UpdateEntityAsync_ShouldUpdateEntity_ReturnTrueIfSuccessfully(TestEntity entity)
        {
            // Arrange
            await _repository.CreateEntityAsync(entity);
            entity.StringValue = "New Value";

            // Act
            var updateResult = await _repository.UpdateEntityAsync(entity);
            var updatedEntity = await _context.Set<TestEntity>().FindAsync(entity.Id);

            // Assert updateResult
            updateResult.Should().NotBeNull();

            // Assert updatedEntity
            updatedEntity.Should().NotBeNull();
            updatedEntity.StringValue.Should().Be("New Value");
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldThrowException_WhenEntityIsNull()
        {
            // Act
            Func<Task> act = async () => await _repository.UpdateEntityAsync(null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory, AutoData]
        public async Task UpdateEntityAsync_ShouldReturnFalse_WhenEntityNotFound(TestEntity entity)
        {
            // Act
            var updatingResult = (await _repository.UpdateEntityAsync(entity)).Value;

            // Assert
            updatingResult.Should().BeNull();
        }

        [Theory, AutoData]
        public async Task RemoveEntityAsync_ShouldRemoveEntityFromDb_ReturnTrueIfSuccessfully(TestEntity entity)
        {
            // Arrange
            await _repository.CreateEntityAsync(entity);

            // Act
            var deletingResult = await _repository.RemoveEntityAsync(entity.Id);
            var existingResult = await _context.Set<TestEntity>().FindAsync(entity.Id);

            // Assert deletingResult
            deletingResult.Value.Should().NotBeNull();

            // Assert existingResult
            existingResult.Should().BeNull();
        }

        [Theory, AutoData]
        public async Task RemoveEntityAsync_ShouldReturnFalse_WhenEntityNotExist(Guid id)
        {
            // Act
            var deletingResult = await _repository.RemoveEntityAsync(id);

            // Assert
            deletingResult.Value.Should().BeNull();
        }

        [Fact]
        public async Task RemoveEntityAsync_ShouldThrowException_WhenEntityIsEmpty()
        {
            // Act
            Func<Task> act = async () => await _repository.RemoveEntityAsync(Guid.Empty);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetOneEntityAsync_ShouldReturnOneEntity_WhenSearchById()
        {
            // Arrange
            var items = await SetupTestEntitiesAsync();
            var id = items.First().Id;
            items.First().RelatedTestEntities = new List<RelatedTestEntity>();
            items.First().AnotherTestEntity = null;

            // Act
            var result = (await _repository.FindEntityAsync(id)).Value;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(items.First());
        }

        [Theory, AutoData]
        public async Task GetOneEntityAsync_ShouldReturnNull_WhenItNotExistById(Guid id)
        {
            // Act
            var result = (await _repository.FindEntityAsync(id)).Value;

            // Assert
            result.Should().BeNull();
        }

        [Theory, AutoData]
        public async Task GetOneEntityAsync_ShouldThrowException_WhenIdEmpty(Guid id)
        {
            // Assert
            id = Guid.Empty;

            // Act
            Func<Task> act = async () => await _repository.FindEntityAsync(id);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetOneEntityAsync_ShouldReturnOneEntity_WhenSearchByExpression()
        {
            // Arrange
            var items = await SetupTestEntitiesAsync();
            var id = items.First().Id;
            items.First().RelatedTestEntities = new List<RelatedTestEntity>();
            items.First().AnotherTestEntity = null;

            // Act
            var result = (await _repository.FindEntityAsync(x => x.Id == id)).Value;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(items.First());
        }

        [Theory, AutoData]
        public async Task GetOneEntityAsync_ShouldReturnNull_WhenItNotExistByExpression(Guid id)
        {
            // Act
            var result = (await _repository.FindEntityAsync(x => x.Id == id)).Value;

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetOneEntityAsync_ShouldThrowException_WhenExpressionEmpty()
        {
            // Act
            Func<Task> act = async () => await _repository.FindEntityAsync(null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        //[Fact]
        //public async Task GetOneEntityAsync_ShouldReturnOneEntity_WhenSearchByExpressionWithIncludeProps()
        //{
        //    // Arrange
        //    var items = await SetupTestEntitiesAsync();
        //    var value = items.First().AnotherTestEntity.TestDoubleValue;
        //    items.First().AnotherTestEntity.DeepRelatedTestEntity = null;

        //    // Act
        //    var result = await _repository.FindEntityAsync(
        //        x => x.AnotherTestEntity.TestDoubleValue == value, x => x...);

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.Should().BeEquivalentTo(items.First());
        //}

        //[Theory, AutoData]
        //public async Task GetOneEntityAsync_ShouldReturnNull_WhenItNotExistByExpressionWithIncludeProps(Guid id)
        //{
        //    // Arrange
        //    var props = new IncludeProperties<TestEntity>(true);

        //    // Act
        //    var result = await _repository.GetOneEntityAsync(x => x.Id == id, props);

        //    // Assert
        //    result.Should().BeNull();
        //}

        //[Fact]
        //public async Task GetOneEntityAsync_ShouldThrowException_WhenExpressionEmptyWithIncludeProps()
        //{
        //    // Arrange
        //    var props = new IncludeProperties<TestEntity>(true);

        //    // Act
        //    Func<Task> act = async () => await _repository.GetOneEntityAsync(null, props);

        //    // Assert
        //    await act.Should().ThrowAsync<ArgumentNullException>();
        //}

        //[Fact]
        //public async Task GetOneEntityAsync_ShouldNotUseEagerLoading_WhenItExistWithoutIncludedCollections()
        //{
        //    // Arrange
        //    IncludeProperties<TestEntity> properties = new(false);
        //    var items = await SetupTestEntitiesAsync();
        //    var id = items.First().Id;
        //    items.First().RelatedTestEntities = new List<RelatedTestEntity>();
        //    items.First().AnotherTestEntity = null;

        //    // Act
        //    var result = await _repository.GetOneEntityAsync(id, properties);

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.Should().BeEquivalentTo(items.First());
        //    result.RelatedTestEntities.Should().BeEmpty();
        //    result.AnotherTestEntity.Should().BeNull();
        //}

        //[Fact]
        //public async Task GetOneEntityAsync_ShouldUseOneEagerLoading_WhenItExistWithIncludedCollections()
        //{
        //    // Arrange
        //    IncludeProperties<TestEntity> properties = new(x => x.RelatedTestEntities);
        //    var items = await SetupTestEntitiesAsync();
        //    var id = items.First().Id;
        //    items.First().AnotherTestEntity = null;

        //    // Act
        //    var result = await _repository.GetOneEntityAsync(id, properties);

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.Should().BeEquivalentTo(items.First());
        //    result.RelatedTestEntities.First().Should().NotBeNull();
        //    result.AnotherTestEntity.Should().BeNull();
        //}

        //[Fact]
        //public async Task GetOneEntityAsync_ShouldUseEveryOnLevelEagerLoading_WhenItExistWithIncludedCollections()
        //{
        //    // Arrange
        //    IncludeProperties<TestEntity> properties = new(true);
        //    var items = await SetupTestEntitiesAsync();
        //    var id = items.First().Id;
        //    items.First().AnotherTestEntity.DeepRelatedTestEntity = null;

        //    // Act
        //    var result = await _repository.GetOneEntityAsync(id, properties);

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.Should().BeEquivalentTo(items.First());
        //    result.RelatedTestEntities.First().Should().NotBeNull();
        //    result.AnotherTestEntity.Should().NotBeNull();
        //    result.AnotherTestEntity.DeepRelatedTestEntity.Should().BeNull();
        //}

        //[Fact]
        //public async Task GetOneEntityAsync_ShouldUseDeepEagerLoading_WhenItExistWithIncludedCollections()
        //{
        //    // Arrange
        //    IncludeProperties<TestEntity> properties = new(
        //        x => x.RelatedTestEntities, 
        //        x => x.AnotherTestEntity,
        //        x => x.AnotherTestEntity.DeepRelatedTestEntity);
        //    var items = await SetupTestEntitiesAsync();
        //    var id = items.First().Id;

        //    // Act
        //    var result = await _repository.GetOneEntityAsync(id, properties);

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.Should().BeEquivalentTo(items.First());
        //    result.RelatedTestEntities.First().Should().NotBeNull();
        //    result.AnotherTestEntity.Should().NotBeNull();
        //    result.AnotherTestEntity.DeepRelatedTestEntity.Should().NotBeNull();
        //}

        //[Fact]
        //public async Task GetManyEntitiesAsync_ReturnQuery_WithoutArguments()
        //{
        //    // Arrange
        //    var items = await SetupTestEntitiesAsync();

        //    // Act
        //    var result = await _repository.GetManyEntitiesAsync();

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.Count().Should().Be(items.Count());
        //    result.First().AnotherTestEntity.Should().BeNull();
        //}

        //[Fact]
        //public async Task GetManyEntitiesAsync_ReturnQuery_WithIncludeProps()
        //{
        //    // Arrange
        //    IncludeProperties<TestEntity> props = new(true);
        //    var items = await SetupTestEntitiesAsync();

        //    // Act
        //    var result = await _repository.GetManyEntitiesAsync(props);

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.Count().Should().Be(items.Count());
        //    result.First().RelatedTestEntities.Should().NotBeNull();
        //    result.First().AnotherTestEntity.Should().NotBeNull();
        //    result.First().AnotherTestEntity.DeepRelatedTestEntity.Should().BeNull();
        //}

        //[Fact]
        //public async Task GetManyEntitiesAsync_ReturnQuery_WithExpressionWithoutIncludeProps()
        //{
        //    // Arrange
        //    var items = _fixture.CreateMany<TestEntity>(5);
        //    int k = 1;
        //    foreach (var item in items)
        //    {
        //        if (k % 2 == 0)
        //            item.AnotherTestEntity.TestDoubleValue = 10.0;
        //        else
        //            item.AnotherTestEntity.TestDoubleValue = -10.0;

        //        await _repository.CreateEntityAsync(item); k++;
        //    }

        //    // Act
        //    var result = await _repository.GetManyEntitiesAsync(x => x.AnotherTestEntity.TestDoubleValue > 0);

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.Count().Should().Be((int)(items.Count() / 2.0));
        //    result.First().AnotherTestEntity.Should().BeNull();
        //}

        //[Fact]
        //public async Task GetManyEntitiesAsync_ReturnQuery_WithExpressionWithIncludeProps()
        //{
        //    // Arrange
        //    IncludeProperties<TestEntity> props = new(true);
        //    var items = _fixture.CreateMany<TestEntity>(5);
        //    int k = 1;
        //    foreach (var item in items)
        //    {
        //        if (k % 2 == 0)
        //            item.AnotherTestEntity.TestDoubleValue = 10.0;
        //        else
        //            item.AnotherTestEntity.TestDoubleValue = -10.0;

        //        await _repository.CreateEntityAsync(item); k++;
        //    }

        //    // Act
        //    var result = await _repository.GetManyEntitiesAsync(x => x.AnotherTestEntity.TestDoubleValue > 0, props);

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.Count().Should().Be((int)(items.Count() / 2.0));
        //    result.First().RelatedTestEntities.Should().NotBeNull();
        //    result.First().AnotherTestEntity.Should().NotBeNull();
        //    result.First().AnotherTestEntity.DeepRelatedTestEntity.Should().BeNull();
        //}

        //[Fact]
        //public async Task GetManyEntitiesAsync_ShouldThrowException_WhenExpressionEmptyWithIncludeProps()
        //{
        //    // Arrange
        //    var props = new IncludeProperties<TestEntity>(true);

        //    // Act
        //    Func<Task> act = async () => await _repository.GetManyEntitiesAsync(null, props);

        //    // Assert
        //    await act.Should().ThrowAsync<ArgumentNullException>();
        //}

        private async Task<IEnumerable<TestEntity>> SetupTestEntitiesAsync()
        {
            var items = _fixture.CreateMany<TestEntity>(5);
            foreach (var item in items)
            {
                await _repository.CreateEntityAsync(item);
            }

            return items;
        }
    }
}
