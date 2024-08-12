using HardwareHero.Shared.Models;
using HardwareHero.Shared.Repositories.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace HardwareHero.Shared.Tests.Repositories.EF
{
    public class EFCrudRepositoryAsync_UnitTests
    {
        //private readonly Fixture _fixture;
        //private readonly Mock<DbContext> _dbContextMock;
        //private readonly Mock<DbSet<BaseEntity>> _dbSetMock;
        //private readonly EFCrudRepositoryAsync<BaseEntity> _repository;

        //public EFCrudRepositoryAsync_UnitTests()
        //{
        //    _fixture = new Fixture();
        //    _dbContextMock = new Mock<DbContext>();
        //    _dbSetMock = new Mock<DbSet<BaseEntity>>();
        //    _dbContextMock.Setup(m => m.Set<BaseEntity>()).Returns(_dbSetMock.Object);
        //    _repository = new EFCrudRepositoryAsync<BaseEntity>(_dbContextMock.Object);
        //}

        //[Fact]
        //public async Task CreateEntityAsync_ShouldReturnEntityId_WhenEntityIsCreatedSuccessfully()
        //{
        //    // Arrange
        //    var entity = _fixture.Create<BaseEntity>();
        //    _dbSetMock.Setup(x => x.AddAsync(entity, It.IsAny<CancellationToken>())).ReturnsAsync((EntityEntry<BaseEntity>) null);
        //    _dbContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        //    // Act
        //    var result = await _repository.CreateEntityAsync(entity);

        //    // Assert
        //    result.Should().Be(entity.Id);
        //    _dbSetMock.Verify(x => x.AddAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
        //    _dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        //}

        //[Fact]
        //public async Task UpdateEntityAsync_ShouldReturnTrue_WhenEntityIsUpdatedSuccessfully()
        //{
        //    // Arrange
        //    var entity = _fixture.Create<BaseEntity>();
        //    _dbContextMock.Setup(x => x.Update(entity)).Returns((EntityEntry<BaseEntity>) null);
        //    _dbContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        //    // Act
        //    var result = await _repository.UpdateEntityAsync(entity);

        //    // Assert
        //    result.Should().BeTrue();
        //    _dbContextMock.Verify(x => x.Update(entity), Times.Once);
        //    _dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        //}


        //[Fact]
        //public async Task RemoveEntityAsync_ShouldReturnTrue_WhenEntityIsDeletedSuccessfully()
        //{
        //    // Arrange
        //    var entity = _fixture.Create<BaseEntity?>();
        //    _dbContextMock.Setup(m => m.FindAsync<BaseEntity>(new object[] { entity.Id }, It.IsAny<CancellationToken>())).ReturnsAsync(entity);
        //    _dbContextMock.Setup(x => x.Remove(entity));
        //    _dbContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        //    // Act
        //    var result = await _repository.RemoveEntityAsync(entity.Id);

        //    // Assert
        //    result.Should().BeTrue();
        //    _dbContextMock.Verify(x => x.FindAsync<BaseEntity>(new object[] { entity.Id }, It.IsAny<CancellationToken>()), Times.Once);
        //    _dbContextMock.Verify(x => x.Remove(entity), Times.Once);
        //    _dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        //}

        //[Fact]
        //public async Task RemoveEntityAsync_ShouldReturnFalse_WhenEntityIsNotExist()
        //{
        //    // Arrange
        //    var entity = _fixture.Create<BaseEntity>();
        //    _dbContextMock.Setup(x => x.FindAsync<BaseEntity>(new object[] { entity.Id },
        //        It.IsAny<CancellationToken>())).ReturnsAsync((BaseEntity?) null);

        //    // Act
        //    var result = await _repository.RemoveEntityAsync(entity.Id);

        //    // Assert
        //    result.Should().BeFalse();
        //    _dbContextMock.Verify(x => x.FindAsync<BaseEntity>(new object[] { entity.Id }, It.IsAny<CancellationToken>()), Times.Once);
        //    _dbContextMock.Verify(x => x.Remove(entity), Times.Never);
        //    _dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        //}
    }
}
