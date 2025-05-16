using MMAPredictor.Core.DTO;
using MMAPredictor.DataScrapper;

namespace Test.MMAPredictor.DataScrapper
{
    public class TestUFCScrapperService
    {
        [Fact]
        public async Task Test_That_Scrapping_UFC_Fighter_Page_Retrieves_His_Name()
        {
            UFCScrapperService service = new UFCScrapperService();
            FighterDTO fighter = await service.ScrapFighterPageAsync("http://ufcstats.com/fighter-details/b1b0729d27936f2f");
            Assert.True(!string.IsNullOrEmpty(fighter?.Name));
        }
    }
}
