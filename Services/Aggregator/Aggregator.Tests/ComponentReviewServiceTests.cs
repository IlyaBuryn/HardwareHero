using Aggregator.BusinessLogic.Contracts;
using Aggregator.BusinessLogic.Services;
using AutoMapper;
using FluentAssertions;
using HardwareHero.Services.Shared.DTOs;
using HardwareHero.Services.Shared.Exceptions;
using HardwareHero.Services.Shared.Models.Aggregator;
using HardwareHero.Services.Shared.Repositories.Contracts;
using Moq;
using System.Linq.Expressions;

namespace Aggregator.Tests
{
    public class ComponentReviewServiceTests
    {
        private readonly IComponentReviewService _componentReviewService;

        public ComponentReviewServiceTests()
        {            
        }

        [Fact]
        public async Task AddComponentReviewAsync_ReturnNotNullValue()
        {
            // Arrange
            var mockRepository = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentReviewService(mockRepository.Object, mockMapper.Object);
            var componentReviewDto = new ComponentReviewDto();

            // Act
            var result = await service.AddComponentReviewAsync(componentReviewDto);

            // Assert
            result.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task GetComponentReviewsAsPageByComponentIdAsync_ReturnNotEmptyList()
        {
            // Arrange
            var mockRepository = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentReviewService(mockRepository.Object, mockMapper.Object);
            var pageNumber = 1;
            var pageSize = 10;
            var componentId = Guid.NewGuid();
            var reviews = new List<ComponentReview> { new ComponentReview() };

            mockRepository.Setup(repo => repo.GetManyEntitiesAsync(It.IsAny<Expression<Func<ComponentReview, bool>>>()))
                .ReturnsAsync(reviews.AsQueryable());
            mockRepository.Setup(repo => repo.GetPageAsync(It.IsAny<IQueryable<ComponentReview>>(), pageNumber, pageSize))
                .ReturnsAsync(reviews);

            // Act
            var result = await service.GetComponentReviewsAsPageByComponentIdAsync(pageNumber, pageSize, componentId);

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetComponentReviewsAsPageByComponentIdAsync_ThrowPageOptionsException()
        {
            // Arrange
            var mockRepository = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentReviewService(mockRepository.Object, mockMapper.Object);
            var pageNumber = 0;
            var pageSize = 0;
            var componentId = Guid.NewGuid();

            // Act & Assert
            await Assert.ThrowsAsync<PageOptionsValidationException>(() =>
                service.GetComponentReviewsAsPageByComponentIdAsync(pageNumber, pageSize, componentId));
        }

        [Fact]
        public async Task GetComponentReviewsAsPageByComponentIdAsync_ReturnEmptyList()
        {
            // Arrange
            var mockRepository = new Mock<IPageRepositoryAsync<ComponentReview>>();
            var mockMapper = new Mock<IMapper>();

            var service = new ComponentReviewService(mockRepository.Object, mockMapper.Object);
            var pageNumber = 1;
            var pageSize = 10;
            var componentId = Guid.NewGuid();
            var reviews = new List<ComponentReview>();

            mockRepository.Setup(repo => repo.GetManyEntitiesAsync(It.IsAny<Expression<Func<ComponentReview, bool>>>()))
                .ReturnsAsync(reviews.AsQueryable());

            // Act
            var result = await service.GetComponentReviewsAsPageByComponentIdAsync(pageNumber, pageSize, componentId);

            // Assert
            result.Should().BeEmpty();
        }
    }
}
