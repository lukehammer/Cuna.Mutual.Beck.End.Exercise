using Cuna.Mutual.Back.End.Exercise.Api.ApiModels;
using Cuna.Mutual.Back.End.Exercise.Api.Controllers;
using Cuna.Mutual.Back.End.Exercise.Api.Data;
using Cuna.Mutual.Back.End.Exercise.Api.Models;
using Cuna.Mutual.Back.End.Exercise.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Cuna.Mutual.Back.End.Exercise.UnitTest
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

            var result = controller.NewRequest(dto);

            //Assert
            var apiResult = Assert.IsType<CreatedResult>(result);
            _mockMacGuffinRepository.Verify(x => x.AddNew(It.IsAny<MacGuffin>()), Times.Once);
            apiResult.Location.Should().EndWith("/callback/0");
        }


        
        [Fact]
        public void CallBack_FromThirdParty_StartedCallBack_ExpectedUpdatedRecord()
        {
            //Arrange

            MacGuffin mcGuffin = new MacGuffin("test");
            _mockMacGuffinRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(mcGuffin);

            var controller = CreateNewController();

            //Act
            var result = controller.StartedCallBack(1);

            //Assert
            var apiResult = Assert.IsType<NoContentResult>(result);
            _mockMacGuffinRepository.Verify(x => x.Update(mcGuffin), Times.AtLeastOnce());
          
        }


        [Fact]
        public void CallBack_FromThirdParty_AddStatus_ExpectedUpdatedRecord()
        {
            //Arrange

            MacGuffin mcGuffin = new MacGuffin("test");
            _mockMacGuffinRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(mcGuffin);

            var controller = CreateNewController();
           


            //Act
            StatusDto status = new StatusDto{Status = "complete", Detail = "just finished"};
            var result = controller.AddStatus(1, status);

            //Assert
            var apiResult = Assert.IsType<NoContentResult>(result);
            _mockMacGuffinRepository.Verify(x => x.Update(mcGuffin), Times.AtLeastOnce());
        }




        [Fact]
        public void Check_getter()
        {
            //Arrange

            MacGuffin mcGuffin = new MacGuffin("test");
            _mockMacGuffinRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(mcGuffin);

            var controller = CreateNewController();

            //Act
         
            var result = controller.GetMacGuffin( 1);

            //Assert


            var apiResult = Assert.IsType<OkObjectResult>(result);
            apiResult.Value.Should().Be(mcGuffin);
        }

    }
}