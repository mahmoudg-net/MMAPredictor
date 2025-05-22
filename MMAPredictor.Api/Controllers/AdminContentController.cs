using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MMAPredictor.Core.DTO;
using MMAPredictor.DataAccess;
using MMAPredictor.DataAccess.Entities;
using MMAPredictor.DataScrapper;
using System.Net;

namespace MMAPredictor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminContentController : ControllerBase
    {
        private readonly IUFCScrapperService _scrappingService;
        private readonly IMapper _mapper;
        private readonly MMAPredictorDbContext _dbContext;
        public AdminContentController(IUFCScrapperService scrappingService ,IMapper mapper, MMAPredictorDbContext dbContext)
        {
            _scrappingService = scrappingService;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpPost("/Fighters/Scrap")]
        public async Task<IActionResult> ScrapFighterURL([FromBody] string fighterURL)
        {
            FighterDTO? fighter = await _scrappingService.ScrapFighterPageAsync(fighterURL, null);
            
            if (fighter is not null)
            {
                var existingFighter = await _dbContext.Fighters.FirstOrDefaultAsync(x => x.Name == fighter.Name);
                if (existingFighter == null)
                {
                    var figherEntity = _mapper.Map<Fighter>(fighter);
                    await _dbContext.Fighters.AddAsync(figherEntity);
                    await _dbContext.SaveChangesAsync();
                    return Created();
                }
                else
                {
                    _mapper.Map(fighter, existingFighter);
                    await _dbContext.SaveChangesAsync();
                    return Ok();
                }
            }
            else
            {
                return UnprocessableEntity();
            }
        }
    }
}
