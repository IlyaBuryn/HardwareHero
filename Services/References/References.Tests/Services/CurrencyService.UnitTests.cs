using HardwareHero.Shared.Repositories.Contracts;
using References.BusinessLogic.Contracts;
using References.BusinessLogic.Models;

namespace References.Tests.Services
{
    public class CurrencyService
    {
        private readonly Fixture _fixture;
        private readonly IMapper _mapper;
        private readonly IBaseRepositoryAsync<Currency> _currencyRepo;
        private readonly ICurrencyService _currencyService;

        public CurrencyService()
        {
            _fixture = new Fixture();
            _mapper = new Mock<IMapper>().Object;
            _currencyRepo = new Mock<IBaseRepositoryAsync<Currency>>().Object;
            _currencyService = new Mock<ICurrencyService>().Object;
        }


        [Fact]
        public void AddCurrency_WhenItsValid_ReturnValidGuid()
        {

        }

        [Fact]
        public void AddCurrency_WhenItsAlreadyExist_ThrowAlreadyExistException()
        {

        }

        [Fact]
        public void UpdateCurrency_WhenItsValid_ReturnTrue()
        {

        }

        [Fact]
        public void UpdateCurrency_WhenTheNewValueAlreadyExist_ThrowAlreadyExistException()
        {

        }

        [Fact]
        public void UpdateCurrency_WhenItsInvalid_ThrowNotFoundException()
        {

        }

        [Fact]
        public void GetCurrenciesList_ReturnFullList()
        {

        }
    }
}
