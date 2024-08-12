using Aggregator.BusinessLogic.Services;
using AutoFixture.Kernel;
using AutoFixture;
using AutoFixture.Xunit2;
using HardwareHero.Shared.DTOs.Aggregator;
using HardwareHero.Shared.Extensions;
using HardwareHero.Shared.Models.Aggregator;
using HardwareHero.Shared.Options;
using HardwareHero.Shared.Repositories.Contracts;
using HardwareHero.Shared.Repositories.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using System.Text;
using System.Linq.Expressions;

namespace Aggregator.Tests.Services
{
    public class ComponentService_UnitTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<ICollectionRepositoryAsync<Component>> _componentRepo;

        private readonly Mock<ICrudRepositoryAsync<ComponentViews>> _componentViewsRepo;
        private readonly Mock<ICrudRepositoryAsync<ComponentType>> _componentTypeRepo;

        private readonly Mock<IValidationRepository<Component>> _componentValidationRepo;
        private readonly Mock<IValidationRepository<ComponentType>> _componentTypeValidationRepo;

        private readonly Mock<IFileRepositoryAsync> _imagesRepo;

        private readonly Mock<IMapper> _mapper;

        private readonly string _fileNameDivider = "_";

        private readonly ComponentService _componentService;

        public ComponentService_UnitTests()
        {
            _fixture = new Fixture();
            _componentRepo = new Mock<ICollectionRepositoryAsync<Component>>();
            _componentViewsRepo = new Mock<ICrudRepositoryAsync<ComponentViews>>();
            _componentTypeRepo = new Mock<ICrudRepositoryAsync<ComponentType>>();
            _componentValidationRepo = new Mock<IValidationRepository<Component>>();
            _componentTypeValidationRepo = new Mock<IValidationRepository<ComponentType>>();
            _imagesRepo = new Mock<IFileRepositoryAsync>();
            _mapper = new Mock<IMapper>();

            var imagesSaveOptions = new ImagesSaveOptions { FileNameDivider = "-", SaveFilePath = "" };
            var imagesOptionsMock = new Mock<IOptions<ImagesSaveOptions>>();
            imagesOptionsMock.Setup(o => o.Value).Returns(imagesSaveOptions);

            _fixture.Customizations.Add(new TypeRelay(typeof(IFormFile), typeof(FormFile)));

            _componentService = new ComponentService(_componentRepo.Object, _componentViewsRepo.Object,
                _componentValidationRepo.Object, _componentTypeValidationRepo.Object,
                _imagesRepo.Object, _mapper.Object, imagesOptionsMock.Object,
                _componentTypeRepo.Object);
        }


        [Theory, CustomAutoData]
        public async Task AddComponentAsync_ReturnNonEmptyGuid_WhenFullComponentAddedSuccessfully(ComponentDto componentToAdd)
        {
            // Arrange
            _componentValidationRepo.Setup(x => x.CheckIfObjectAlreadyExist(
                It.IsAny<Expression<Func<Component, bool>>>(), It.IsAny<string>()));
            _componentTypeValidationRepo.Setup(x => x.CheckIfObjectNotFound(
                It.IsAny<Expression<Func<ComponentType, bool>>>()));
            _imagesRepo.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>(), It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<string>());
            _mapper.Setup(x => x.Map<Component>(componentToAdd)).Returns(It.IsAny<Component>());
            _componentRepo.Setup(x => x.CreateEntityAsync(It.IsAny<Component>())).ReturnsAsync(componentToAdd.Id);

            // Act
            var result = await _componentService.AddComponentAsync(componentToAdd);

            // Assert
            result.Should().Be(componentToAdd.Id);
            _componentValidationRepo.Verify(x => x.CheckIfObjectAlreadyExist(
                It.IsAny<Expression<Func<Component, bool>>>(), It.IsAny<string>()), Times.Once);
            _componentTypeValidationRepo.Verify(x => x.CheckIfObjectNotFound(
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
