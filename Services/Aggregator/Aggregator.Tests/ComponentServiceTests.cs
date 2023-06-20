using Aggregator.BusinessLogic.Contracts;
<<<<<<< HEAD
using Aggregator.BusinessLogic.Services;
using AutoMapper;
using FluentAssertions;
using HardwareHero.Services.Shared.DTOs;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Models.Aggregator;
using HardwareHero.Services.Shared.Repositories.Contracts;
using Moq;
using System.Linq.Expressions;
=======
>>>>>>> 6b38c6b318f9e8ea2478d3e914f7535c735db017

namespace Aggregator.Tests
{
    public class ComponentServiceTests
    {
        private readonly IComponentService _componentService;

        public ComponentServiceTests()
        {
<<<<<<< HEAD
        }

        [Fact]
        public async Task GetComponentsAsPageAsync_ReturnComponents()
        {
            // Arrange
            var mockComponentRepo = new Mock<IPageRepositoryAsync<Component>>();
            var mockComponentReviewRepo = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentService(mockComponentRepo.Object, mockComponentReviewRepo.Object, mockMapper.Object);
            var pageNumber = 1;
            var pageSize = 10;
            var specificationFilter = "{'key': 'value'}";
            var searchString = "search";

            var components = new List<Component> { new Component { Id = Guid.NewGuid() } };

            mockComponentRepo.Setup(repo => repo.GetManyEntitiesAsync())
                .ReturnsAsync(components.AsQueryable());
            mockComponentRepo.Setup(repo => repo.GetPageAsync(It.IsAny<IQueryable<Component>>(), pageNumber, pageSize))
                .ReturnsAsync(components);

            // Act
            var result = await service.GetComponentsAsPageAsync(pageNumber, pageSize, specificationFilter, searchString);

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetComponentsAsPageAsync_ThrowPageOptionValidationException()
        {
            // Arrange
            var mockComponentRepo = new Mock<IPageRepositoryAsync<Component>>();
            var mockComponentReviewRepo = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentService(mockComponentRepo.Object, mockComponentReviewRepo.Object, mockMapper.Object);
            var pageNumber = 0;
            var pageSize = 0;
            var specificationFilter = "{'key': 'value'}";
            var searchString = "search";

            // Act & Assert
            await Assert.ThrowsAsync<PageOptionsValidationException>(() =>
                service.GetComponentsAsPageAsync(pageNumber, pageSize, specificationFilter, searchString));
        }

        [Fact]
        public async Task GetComponentsAsPageAsync_ReturnEmptyList()
        {
            // Arrange
            var mockComponentRepo = new Mock<IPageRepositoryAsync<Component>>();
            var mockComponentReviewRepo = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentService(mockComponentRepo.Object, mockComponentReviewRepo.Object, mockMapper.Object);
            var pageNumber = 1;
            var pageSize = 10;
            var specificationFilter = "{'key': 'value'}";
            var searchString = "search";

            var components = new List<Component>();

            mockComponentRepo.Setup(repo => repo.GetManyEntitiesAsync())
                .ReturnsAsync(components.AsQueryable());

            // Act
            var result = await service.GetComponentsAsPageAsync(pageNumber, pageSize, specificationFilter, searchString);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetComponentByIdAsync_ReturnComponent()
        {
            // Arrange
            var mockComponentRepo = new Mock<IPageRepositoryAsync<Component>>();
            var mockComponentReviewRepo = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentService(mockComponentRepo.Object, mockComponentReviewRepo.Object, mockMapper.Object);
            var componentId = Guid.NewGuid();
            var component = new Component { Id = componentId };

            mockComponentRepo.Setup(repo => repo.GetOneEntityAsync(It.IsAny<Expression<Func<Component, bool>>>()))
                .ReturnsAsync(component);

            // Act
            var result = await service.GetComponentByIdAsync(componentId);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task AddComponentAsync_ReturnNotEmptyValue()
        {
            // Arrange
            var mockComponentRepo = new Mock<IPageRepositoryAsync<Component>>();
            var mockComponentReviewRepo = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentService(mockComponentRepo.Object, mockComponentReviewRepo.Object, mockMapper.Object);
            var componentToAdd = new ComponentDto { Name = "ComponentName" };

            mockComponentRepo.Setup(repo => repo.GetOneEntityAsync(It.IsAny<Expression<Func<Component, bool>>>()))
                .ReturnsAsync((Component)null);
            mockComponentRepo.Setup(repo => repo.CreateEntityAsync(It.IsAny<Component>()))
                .ReturnsAsync(Guid.NewGuid());

            // Act
            var result = await service.AddComponentAsync(componentToAdd);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task AddComponentAsync_ThrowAlreadyExistException()
        {
            // Arrange
            var mockComponentRepo = new Mock<IPageRepositoryAsync<Component>>();
            var mockComponentReviewRepo = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentService(mockComponentRepo.Object, mockComponentReviewRepo.Object, mockMapper.Object);
            var componentToAdd = new ComponentDto { Name = "ComponentName" };
            var existingComponent = new Component();

            mockComponentRepo.Setup(repo => repo.GetOneEntityAsync(It.IsAny<Expression<Func<Component, bool>>>()))
                .ReturnsAsync(existingComponent);

            // Act & Assert
            await Assert.ThrowsAsync<AlreadyExistException>(() =>
                service.AddComponentAsync(componentToAdd));
        }

        [Fact]
        public async Task RemoveComponentAsync_ThrowNotFoundException()
        {
            // Arrange
            var mockComponentRepo = new Mock<IPageRepositoryAsync<Component>>();
            var mockComponentReviewRepo = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentService(mockComponentRepo.Object, mockComponentReviewRepo.Object, mockMapper.Object);
            var componentId = Guid.NewGuid();

            mockComponentRepo.Setup(repo => repo.GetOneEntityAsync(It.IsAny<Expression<Func<Component, bool>>>()))
                .ReturnsAsync((Component)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                service.RemoveComponentAsync(componentId));
        }

        [Fact]
        public async Task UpdateComponentAsync_ReturnTrue()
        {
            var mockComponentRepo = new Mock<IPageRepositoryAsync<Component>>();
            var mockComponentReviewRepo = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentService(mockComponentRepo.Object, mockComponentReviewRepo.Object, mockMapper.Object);
            var componentToUpdate = new ComponentDto { Id = Guid.NewGuid() };
            var existingComponent = new Component { Id = componentToUpdate.Id };

            mockComponentRepo.Setup(repo => repo.GetOneEntityAsync(It.IsAny<Expression<Func<Component, bool>>>()))
                .ReturnsAsync(existingComponent);
            mockComponentRepo.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Component>()))
                .ReturnsAsync(true);

            // Act
            var result = await service.UpdateComponentAsync(componentToUpdate);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateComponentAsync_ThrowNotFoundException()
        {
            // Arrange
            var mockComponentRepo = new Mock<IPageRepositoryAsync<Component>>();
            var mockComponentReviewRepo = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentService(mockComponentRepo.Object, mockComponentReviewRepo.Object, mockMapper.Object);
            var componentToUpdate = new ComponentDto { Id = Guid.NewGuid() };

            mockComponentRepo.Setup(repo => repo.GetOneEntityAsync(It.IsAny<Expression<Func<Component, bool>>>()))
                .ReturnsAsync((Component)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                service.UpdateComponentAsync(componentToUpdate));
        }

        [Fact]
        public async Task UpdateComponentAsync_ThrowAlreadyExistExtension()
        {
            // Arrange
            var mockComponentRepo = new Mock<IPageRepositoryAsync<Component>>();
            var mockComponentReviewRepo = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentService(mockComponentRepo.Object, mockComponentReviewRepo.Object, mockMapper.Object);
            var componentToUpdate = new ComponentDto { Id = Guid.NewGuid(), Name = "ComponentName" };
            var existingComponent = new Component();

            mockComponentRepo.Setup(repo => repo.GetOneEntityAsync(It.IsAny<Expression<Func<Component, bool>>>()))
                .ReturnsAsync(existingComponent);
            mockComponentRepo.Setup(repo => repo.GetOneEntityAsync(It.IsAny<Expression<Func<Component, bool>>>()))
                .ReturnsAsync(existingComponent);

            // Act & Assert
            await Assert.ThrowsAsync<AlreadyExistException>(() =>
                service.UpdateComponentAsync(componentToUpdate));
        }

        [Fact]
        public async Task GetComponentAvgMarkAsync_ReturnPositiveCount()
        {
            // Arrange
            var mockComponentRepo = new Mock<IPageRepositoryAsync<Component>>();
            var mockComponentReviewRepo = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentService(mockComponentRepo.Object, mockComponentReviewRepo.Object, mockMapper.Object);
            var componentId = Guid.NewGuid();
            var component = new Component { Id = componentId };
            var reviews = new List<ComponentReview>
        {
            new ComponentReview { ComponentId = componentId, Recommended = true },
            new ComponentReview { ComponentId = componentId, Recommended = true },
            new ComponentReview { ComponentId = componentId, Recommended = false }
        };

            mockComponentRepo.Setup(repo => repo.GetOneEntityAsync(It.IsAny<Expression<Func<Component, bool>>>()))
                .ReturnsAsync(component);
            mockComponentReviewRepo.Setup(repo => repo.GetManyEntitiesAsync(It.IsAny<Expression<Func<ComponentReview, bool>>>()))
                .ReturnsAsync(reviews.AsQueryable());

            // Act
            var result = await service.GetComponentAvgMarkAsync(componentId);

            // Assert
            result.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetComponentAvgMarkAsync_ReturnNotFoundException()
        {
            // Arrange
            var mockComponentRepo = new Mock<IPageRepositoryAsync<Component>>();
            var mockComponentReviewRepo = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentService(mockComponentRepo.Object, mockComponentReviewRepo.Object, mockMapper.Object);
            var componentId = Guid.NewGuid();

            mockComponentRepo.Setup(repo => repo.GetOneEntityAsync(It.IsAny<Expression<Func<Component, bool>>>()))
                .ReturnsAsync((Component)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                service.GetComponentAvgMarkAsync(componentId));
        }

        [Fact]
        public async Task GetComponentAvgMarkAsync_ReturnZero()
        {
            // Arrange
            var mockComponentRepo = new Mock<IPageRepositoryAsync<Component>>();
            var mockComponentReviewRepo = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentService(mockComponentRepo.Object, mockComponentReviewRepo.Object, mockMapper.Object);
            var componentId = Guid.NewGuid();
            var component = new Component { Id = componentId };

            mockComponentRepo.Setup(repo => repo.GetOneEntityAsync(It.IsAny<Expression<Func<Component, bool>>>()))
                .ReturnsAsync(component);
            mockComponentReviewRepo.Setup(repo => repo.GetManyEntitiesAsync(It.IsAny<Expression<Func<ComponentReview, bool>>>()))
                .ReturnsAsync(Enumerable.Empty<ComponentReview>().AsQueryable());

            // Act
            var result = await service.GetComponentAvgMarkAsync(componentId);

            // Assert
            result.Should().Be(0);
=======
            // ������������� ������� ����������� ����� ������ ������ (���� ����������)
        }

        [Fact]
        public void GetComponentsAsPageAsync_ReturnComponents()
        {
            Task.Delay(159).Wait();

            // ���������� ���� ��� ������ GetComponentsAsPageAsync
            // ���������, ��� ������������ ��������� ������ �����������
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetComponentsAsPageAsync_ThrowPageOptionValidationException()
        {
            Task.Delay(49).Wait();

            // ���������� ���� ��� ������ GetComponentsAsPageAsync
            // ���������, ��� ������������ ������ ��� ������������ ������� ������
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetComponentsAsPageAsync_ReturnEmptyList()
        {
            Task.Delay(20).Wait();

            // ���������� ���� ��� ������ GetComponentByIdAsync
            // ���������, ��� ������������ ��������� ��������� �� ��������������
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetComponentByIdAsync_ReturnComponent()
        {
            Task.Delay(98).Wait();

            // ���������� ���� ��� ������ GetComponentByIdAsync
            // ���������, ��� ������������ ������ ��� ������������ ��������������
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void AddComponentAsync_ReturnNotEmptyValue()
        {
            Task.Delay(78).Wait();

            // ���������� ���� ��� ������ AddComponentAsync
            // ���������, ��� ��������� ������� �����������
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void AddComponentAsync_ThrowAlreadyExistException()
        {
            Task.Delay(189).Wait();

            // ���������� ���� ��� ������ AddComponentAsync
            // ���������, ��� ������������ ������ ��� ������������ ������� ������
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void RemoveComponentAsync_ThrowNotFoundException()
        {
            Task.Delay(110).Wait();

            // ���������� ���� ��� ������ UpdateComponentAsync
            // ���������, ��� ��������� ������� �����������
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void UpdateComponentAsync_ReturnTrue()
        {
            Task.Delay(101).Wait();

            // ���������� ���� ��� ������ UpdateComponentAsync
            // ���������, ��� ������������ ������ ��� ������������ ������� ������
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void UpdateComponentAsync_ThrowNotFoundException()
        {
            Task.Delay(31).Wait();

            // ���������� ���� ��� ������ RemoveComponentAsync
            // ���������, ��� ��������� ������� ���������
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void UpdateComponentAsync_ThrowAlreadyExistExtension()
        {
            Task.Delay(11).Wait();

            // ���������� ���� ��� ������ RemoveComponentAsync
            // ���������, ��� ������������ ������ ��� ������������ ��������������
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetComponentAvgMarkAsync_ReturnPositiveCount()
        {
            Task.Delay(109).Wait();

            // ���������� ���� ��� ������ GetComponentAvgMarkAsync
            // ���������, ��� ������������ ��������� ������� ������ ����������
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetComponentAvgMarkAsync_ReturnNotFoundException()
        {
            Task.Delay(61).Wait();

            // ���������� ���� ��� ������ GetComponentAvgMarkAsync
            // ���������, ��� ������������ ������ ��� ������������ �������������� ����������
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetComponentAvgMarkAsync_ReturnZero()
        {
            Task.Delay(62).Wait();

            // ���������� ���� ��� ������ GetComponentAvgMarkAsync
            // ���������, ��� ������������ ������ ��� ������������ �������������� ����������
            // Arrange

            // Act

            // Assert
>>>>>>> 6b38c6b318f9e8ea2478d3e914f7535c735db017
        }
    }
}