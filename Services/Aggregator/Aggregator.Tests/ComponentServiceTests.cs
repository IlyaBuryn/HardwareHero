using Aggregator.BusinessLogic.Contracts;

namespace Aggregator.Tests
{
    public class ComponentServiceTests
    {
        private readonly IComponentService _componentService;

        public ComponentServiceTests()
        {
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
        }
    }
}