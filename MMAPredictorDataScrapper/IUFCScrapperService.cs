using MMAPredictor.Core.DTO;

namespace MMAPredictor.DataScrapper
{
    public interface IUFCScrapperService
    {
        public Task<FighterDTO?> ScrapFighterPageAsync(string url);
    }
}
