using MMAPredictor.Core.DTO;
using MMAPredictor.DataScrapper;

namespace Test.MMAPredictor.DataScrapper
{
    public class TestUFCScrapperService
    {
        private const string BELAL_FILE_PATH = "./Belal.html";
        private const string BELAL_URL = "http://ufcstats.com/fighter-details/b1b0729d27936f2f";

        [Fact]
        public async Task Test_That_Scrapping_UFC_Fighter_Page_Retrieves_His_Name()
        {
            UFCScrapperService service = new UFCScrapperService();
            FighterDTO fighter = await service.ScrapFighterPageAsync(null, BELAL_FILE_PATH);
            Assert.True(!string.IsNullOrEmpty(fighter?.Name));
        }

        [Fact]
        public async Task Test_Save_UFCStats_Page_To_Disk()
        {
            UFCScrapperService service = new UFCScrapperService();
            var result = await service.SaveUrlToFile(BELAL_URL, BELAL_FILE_PATH);
            Assert.True(result);
        }

        [Fact]
        public async Task Test_That_Scrapping_UFC_Fighter_Page_Retrieves_His_Record()
        {
            UFCScrapperService service = new UFCScrapperService();
            FighterDTO fighter = await service.ScrapFighterPageAsync(null, BELAL_FILE_PATH);
            Assert.True(fighter.NbWins + fighter.NbLoss + fighter.NbDraws > 0);
        }

        [Fact]
        public async Task Test_That_Scrapping_UFC_Fighter_Page_Retrieves_His_Physique()
        {
            UFCScrapperService service = new UFCScrapperService();
            FighterDTO fighter = await service.ScrapFighterPageAsync(null, BELAL_FILE_PATH);
            Assert.True(fighter.Height > 0 && fighter.Weight > 0 && fighter.Reach > 0 && !string.IsNullOrEmpty(fighter.Stance) && fighter.DateOfBirth.HasValue);
        }

        [Fact]
        public async Task Test_That_Scrapping_UFC_Fighter_Page_Retrieves_His_CareerStatistics()
        {
            UFCScrapperService service = new UFCScrapperService();
            FighterDTO fighter = await service.ScrapFighterPageAsync(null, BELAL_FILE_PATH);
            Assert.True(fighter.StrikesLandedByMinute > 0 && fighter.StrikesAccuracy > 0 && fighter.StrikesAbsorbedByMinute > 0
                && fighter.StrikingDefenceAccuracy > 0 && fighter.TakedownAverage > 0 && fighter.TakedownAccuracy > 0
                && fighter.TakedownDefenceAccuracy > 0 && fighter.SubmissionsAverage > 0);
        }
    }
}
