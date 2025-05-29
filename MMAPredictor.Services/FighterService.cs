using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MMAPredictor.Core.DTO;
using MMAPredictor.Core.ProcessingResult;
using MMAPredictor.DataAccess;
using MMAPredictor.DataAccess.Entities;
using MMAPredictor.DataScrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPredictor.Services
{
    public class FighterService : IFighterService
    {
        private readonly MMAPredictorDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUFCScrapperService _scrappingService;

        public FighterService(IUFCScrapperService scrappingService, IMapper mapper, MMAPredictorDbContext dbContext)
        {
            _scrappingService = scrappingService;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<EntityProcessingResult<Fighter>> UpsertFighterFromUrl(string url)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    return EntityProcessingResult<Fighter>.BadInput("The provided url is empty");
                }

                FighterDTO? fighterDto = await _scrappingService.ScrapFighterPageAsync(url, null);
                if (fighterDto is null)
                {
                    return EntityProcessingResult<Fighter>.NotFound("Error when scrapping the fighter info from the provided URL");
                }

                Fighter? existingFighter = await _dbContext.Fighters.FirstOrDefaultAsync(f => f.Name.ToLowerInvariant() == fighterDto.Name.ToLowerInvariant()   );
                if (existingFighter is null)
                {
                    Fighter fighter = _mapper.Map<Fighter>(fighterDto);
                    await _dbContext.Fighters.AddAsync(fighter);
                    await _dbContext.SaveChangesAsync();
                    return EntityProcessingResult<Fighter>.Created("Fighter created in DB", fighter);
                }
                else
                {
                    _mapper.Map(fighterDto, existingFighter);
                    await _dbContext.SaveChangesAsync();
                    return EntityProcessingResult<Fighter>.Updated("Fighter updated in DB", existingFighter);
                }
            }
            catch(Exception ex)
            {
                return EntityProcessingResult<Fighter>.Exception(ex.ToString());
            }
        }
    }
}
