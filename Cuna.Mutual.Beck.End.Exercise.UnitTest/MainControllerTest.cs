using Cuna.Mutual.Beck.End.Exercise.Api.Controllers;
using Cuna.Mutual.Beck.End.Exercise.Api.Data;
using Cuna.Mutual.Beck.End.Exercise.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Cuna.Mutual.Beck.End.Exercise.UnitTest
{
    public class MainControllerTest
    {
        public MainControllerTest()
        {
            var moqRepository = new MockRepository(MockBehavior.Loose);
            _mockLogger = moqRepository.Create<ILogger<MacGuffinController>>();
            _mockThirdPartyService = moqRepository.Create<IThirdPartyService>();
            _mockMacGuffinRepository = moqRepository.Create<IMacGuffinRepository>();
            _mockHttpContextAccessor = moqRepository.Create<IHttpContextAccessor>();
        }

        private readonly Mock<ILogger<MacGuffinController>> _mockLogger;
        private readonly Mock<IThirdPartyService> _mockThirdPartyService;
        private readonly Mock<IMacGuffinRepository> _mockMacGuffinRepository;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        private MacGuffinController CreateNewController()
        {
            var context = new DefaultHttpContext();
            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);


            return new MacGuffinController(_mockLogger.Object, _mockThirdPartyService.Object,
                _mockMacGuffinRepository.Object, _mockHttpContextAccessor.Object);
        }

        [Fact]
        public void NewRequest_callsPostToThirdPartyService()
        {
            //Arrange

            var controller = CreateNewController();
            var dto = new MacGuffinDto { Body = "test string" };

            //Act

            controller.NewRequest(dto);


            //Assert

            _mockMacGuffinRepository.Verify(x => x.AddNew(It.IsAny<MacGuffin>()), Times.Once);
        }

        

        [Fact]
        public void NewRequest_SendsToRepository()
        {
            //Arrange

            var controller = CreateNewController();
            var dto = new MacGuffinDto {Body = "test string"};

            //Act

            controller.NewRequest(dto);

            //Assert

            _mockMacGuffinRepository.Verify(x => x.AddNew(It.IsAny<MacGuffin>()), Times.Once);
        }


        
        [Fact]
        public void UoW_InitialCondition_ExpectedResult()
        {
            //Arrange


            //Act


            //Assert

        }
    }
}