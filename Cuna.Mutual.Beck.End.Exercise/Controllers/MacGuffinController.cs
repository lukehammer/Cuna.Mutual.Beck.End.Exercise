using System;
using Cuna.Mutual.Beck.End.Exercise.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cuna.Mutual.Beck.End.Exercise.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MacGuffinController : ControllerBase
    {
        private readonly ILogger<MacGuffinController> _logger;
        private readonly IMacGuffinRepository _macGuffinRepository;
        private readonly IThirdPartyService _thirdPartyService;

        public MacGuffinController(ILogger<MacGuffinController> logger, IThirdPartyService thirdPartyService,
            IMacGuffinRepository macGuffinRepository)
        {
            _logger = logger;
            _thirdPartyService = thirdPartyService;
            _macGuffinRepository = macGuffinRepository;
        }

        [HttpPost]
        [Route("/Request")]
        public ActionResult NewRequest(MacGuffinDto incomingDto)
        {
            var macGuffin = new MacGuffin(incomingDto.Body);
            _thirdPartyService.Post(macGuffin);
            _macGuffinRepository.AddNew(macGuffin);

            var statusURi = new UriBuilder
            {
                Host = Request.Host.Host,
                Path = $"status/{macGuffin.Id}"
            };
            var userCallBack = statusURi.Uri;
            return Created(userCallBack, macGuffin);
        }


        [HttpPost]
        [Route("/callback/{id}")]
        public ActionResult PostRequest(string status, Guid id)
        {
            var macGuffin = _macGuffinRepository.Get(id);
            macGuffin.UpdateStatus(status);
            _macGuffinRepository.Update(macGuffin);
            return NoContent();
        }


        [HttpPut]
        [Route("/callback/{id}")]
        public ActionResult Put(MacGuffinPutDto macGuffinPutDto, Guid id)
        {
            var macGuffin = _macGuffinRepository.Get(id);
            macGuffin.UpdateStatus(macGuffinPutDto);
            _macGuffinRepository.Update(macGuffin);
            return NoContent();
        }


        [HttpPut]
        [Route("/status/{id}")]
        public ActionResult Put(Guid id)
        {
            var macGuffin = _macGuffinRepository.Get(id);
          
            return Ok(macGuffin);
        }



    }


    public class MacGuffinPutDto
    {
        public string Status { get; set; }
        public string Detail { get; set; }
        public string Body { get; set; }
    }


    public class MacGuffin
    {
        public MacGuffin(string body)
        {
            Body = body;
            Id = Guid.NewGuid();
        }

        public string Body { get; }
        public Guid Id { get; }

        public void UpdateStatus(MacGuffinPutDto status)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus(string status)
        {
            throw new NotImplementedException();
        }
    }

    public interface IThirdPartyService
    {
        void Post(string body, Uri callBackUri);
        void Post(MacGuffin macGuffin);
    }

    public class MacGuffinDto
    {
        public string Body { get; set; }
    }
}