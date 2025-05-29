using MMAPredictor.Core.DTO;

namespace MMAPredictor.DataScrapper.Interface
{
    public interface IUFCFighterScrapperService
    {
        /// <summary>
        /// If the path to a local file is provided, it will be given priority vs the url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public Task<FighterDTO?> ScrapFighterPageAsync(string? url, string? path);
    }
}
