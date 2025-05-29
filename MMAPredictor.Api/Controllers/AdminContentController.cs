using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MMAPredictor.Core.DTO;
using MMAPredictor.Core.ProcessingResult;
using MMAPredictor.DataAccess;
using MMAPredictor.DataAccess.Entities;
using MMAPredictor.DataScrapper;
using MMAPredictor.Services;
using System.Net;

namespace MMAPredictor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminContentController : ControllerBase
    {
        IFighterService _fighterService;
        public AdminContentController(IFighterService fighterService)
        {
            _fighterService = fighterService;
        }

        [HttpPost("/Fighters/Scrap")]
        public async Task<IActionResult> ScrapFighterURL([FromBody] string fighterURL)
        {
            EntityProcessingResult<Fighter> processingResult = await _fighterService.UpsertFighterFromUrl(fighterURL);
            switch(processingResult.Status)
            {
                case ProcessingStatus.BadInput:
                    return BadRequest();
                case ProcessingStatus.NotFound:
                    return UnprocessableEntity();
                case ProcessingStatus.Created:
                    return CreatedAtAction("Fighters/Scrap", processingResult.Entity);
                case ProcessingStatus.Updated:
                    return Ok(processingResult.Entity);
                case ProcessingStatus.Exception:
                    return Problem(detail: processingResult.Message);
                default:
                    return null;
            }

        }
    }
}
