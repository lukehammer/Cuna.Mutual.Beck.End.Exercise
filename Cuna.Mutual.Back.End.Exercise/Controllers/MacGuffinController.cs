using System;
using System.IO;
using System.IO.Pipelines;
using System.Text;
using Cuna.Mutual.Back.End.Exercise.Api.ApiModels;
using Cuna.Mutual.Back.End.Exercise.Api.Data;
using Cuna.Mutual.Back.End.Exercise.Api.Models;
using Cuna.Mutual.Back.End.Exercise.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

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
                Path = $"callback/{macGuffin.Id}", 
                Host = _httpContextAccessor.HttpContext.Request.Host.Host
            };

            if (_httpContextAccessor.HttpContext.Request.Host.Port.HasValue)
            {statusURi.Port = (int) _httpContextAccessor.HttpContext.Request.Host.Port;}


            var userCallBack = statusURi.Uri;
            return Created(userCallBack, macGuffin);
        }



        [HttpPost]
        [Route("/callback/{id}")]
        public ActionResult StartedCallBack(int id)
        {
            var macGuffin = _macGuffinRepository.Get(id);
            var status = new Status()
            {
                State = "Started"
            };
            macGuffin.UpdateStatus(status);
            _macGuffinRepository.Update(macGuffin);
            return NoContent();
        }




        [HttpPut]
        [Route("/callback/{id}")]
        public ActionResult AddStatus(int id, [FromBody] StatusDto statusDto)
        {

            var macGuffin = _macGuffinRepository.Get(id);
            Status status = new Status()
                {
                    Detail = statusDto.Detail,
                    State = statusDto.Status
                };
            

            macGuffin.UpdateStatus(status);
            _macGuffinRepository.Update(macGuffin);
            return NoContent();
        }


        [HttpGet]
        [Route("/status/{id}")]
        public ActionResult GetMacGuffin(int id)
        {
            var macGuffin = _macGuffinRepository.Get(id);

            return Ok(macGuffin);
        }
    }
}