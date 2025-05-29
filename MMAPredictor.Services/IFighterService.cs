using MMAPredictor.Core.ProcessingResult;
using MMAPredictor.DataAccess.Entities;

namespace MMAPredictor.Services
{
    public interface IFighterService
    {
        public Task<EntityProcessingResult<Fighter>> UpsertFighterFromUrl(string url);
    }
}
