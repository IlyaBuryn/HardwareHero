using Aggregator.BusinessLogic.Services;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using HardwareHero.Shared.Extensions;
using HardwareHero.Shared.Extensions.Repository;
using HardwareHero.Shared.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Text;
using System.Linq.Expressions;
using Aggregator.DataAccess.Models.Components;
using Aggregator.DTOs.Components;
using Moq;
using HardwareHero.Shared.Repositories.Answers;

namespace Aggregator.Tests.Services
{
    public class ComponentService_UnitTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IQueryRepositoryAsync<Component>> _componentRepo;

        private readonly Mock<IBaseRepositoryAsync<ComponentMetric>> _componentMetricRepo;
        private readonly Mock<IBaseRepositoryAsync<ComponentType>> _componentTypeRepo;

        private readonly Mock<IFileRepositoryAsync> _imagesRepo;

        private readonly Mock<IMapper> _mapper;

        private readonly ComponentService _componentService;

        public ComponentService_UnitTests()
        {
            _fixture = new Fixture();
            _componentRepo = new Mock<IQueryRepositoryAsync<Component>>();
            _componentMetricRepo = new Mock<IBaseRepositoryAsync<ComponentMetric>>();
            _componentTypeRepo = new Mock<IBaseRepositoryAsync<ComponentType>>();
            _imagesRepo = new Mock<IFileRepositoryAsync>();
            _mapper = new Mock<IMapper>();

            _fixture.Customizations.Add(new TypeRelay(typeof(IFormFile), typeof(FormFile)));

            _componentService = new ComponentService(_componentRepo.Object, _componentMetricRepo.Object,
                _componentTypeRepo.Object,
                _imagesRepo.Object, _mapper.Object);
        }


        [Theory, CustomAutoData]
        public async Task AddComponentAsync_ReturnNonEmptyGuid_WhenFullComponentAddedSuccessfully(ComponentDto componentToAdd)
        {
            // Arrange
            _componentRepo.Setup(x => x.AlreadyExistCheckAsync(
                It.IsAny<Expression<Func<Component, bool>>>()));
            _componentTypeRepo.Setup(x => x.NotFoundCheckAsync(
                It.IsAny<Expression<Func<ComponentType, bool>>>()));
            _imagesRepo.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<DataAnswer<string>>());
            _mapper.Setup(x => x.Map<Component>(componentToAdd)).Returns(It.IsAny<Component>());
            _componentRepo.Setup(x => x.CreateEntityAsync(It.IsAny<Component>()))
                .ReturnsAsync(It.IsAny<DataAnswer<Component>>);

            // Act
            var result = await _componentService.AddComponentAsync(componentToAdd);

            // Assert
            result.Should().Be(componentToAdd.Id);
            _componentRepo.Verify(x => x.AlreadyExistCheckAsync(
                It.IsAny<Expression<Func<Component, bool>>>()), Times.Once);
            _componentTypeRepo.Verify(x => x.NotFoundCheckAsync(
                It.IsAny<Expression<Func<ComponentType, bool>>>()), Times.Once);
            _imagesRepo.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()),
                Times.Exactly(componentToAdd.ComponentImages.Count));
            _mapper.Verify(x => x.Map<Component>(componentToAdd), Times.Once);
            _componentRepo.Verify(x => x.CreateEntityAsync(It.IsAny<Component>()), Times.Once);
        }



    }

    public class FormFileCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register<IFormFile>(() => CreateFormFile());

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        private IFormFile CreateFormFile()
        {
            var content = "This is a test file";
            var fileName = "test.txt";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            return new FormFile(stream, 0, stream.Length, "formFile", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };
        }
    }

    public class CustomAutoDataAttribute : AutoDataAttribute
    {
        public CustomAutoDataAttribute()
            : base(() => new Fixture().Customize(new FormFileCustomization()))
        {
        }
    }
}
