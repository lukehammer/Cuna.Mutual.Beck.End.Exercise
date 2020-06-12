using System;
using Cuna.Mutual.Back.End.Exercise.Api.Data;
using Cuna.Mutual.Back.End.Exercise.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cuna.Mutual.Back.End.Exercise.Api.Controllers
{
    // Commenting out the authorized attribute as setting up security at this level is not within scope of this code challenge.  
    //[Authorize]


    [ApiController]
    [Route("[controller]")]
    public class MacGuffinController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<MacGuffinController> _logger;
        private readonly IMacGuffinRepository _macGuffinRepository;
        private readonly IThirdPartyService _thirdPartyService;

        public MacGuffinController(ILogger<MacGuffinController> logger, IThirdPartyService thirdPartyService,
            IMacGuffinRepository macGuffinRepository, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _thirdPartyService = thirdPartyService;
            _httpContextAccessor = httpContextAccessor;
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
                Path = $"status/{macGuffin.Id}", 
                Host = _httpContextAccessor.HttpContext.Request.Host.Host
            };

            if (_httpContextAccessor.HttpContext.Request.Host.Port.HasValue)
            {statusURi.Port = (int) _httpContextAccessor.HttpContext.Request.Host.Port;}


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


        [HttpGet]
        [Route("/status/{id}")]
        public ActionResult Get(Guid id)
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


    public class MacGuffinDto
    {
        public string Body { get; set; }
    }
}