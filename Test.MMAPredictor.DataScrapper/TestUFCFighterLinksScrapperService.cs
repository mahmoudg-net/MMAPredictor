using MMAPredictor.DataScrapper.Interface;
using MMAPredictor.DataScrapper.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.MMAPredictor.DataScrapper
{
    public class TestUFCFighterLinksScrapperService
    {
        [Fact]
        public async Task Test_That_Scrapping_A_Page_With_Fighter_Links_Works()
        {
            IUFCFighterLinksScrapperService service = new UFCFighterLinksScrapperService();
            List<Uri> links = new List<Uri>();
            await foreach (Uri link in service.ExtractFighterLinksStartingWithLetter('A', null, "./Ressources/UFC_Links_Letter_A.htm"))
            {
                links.Add(link);
            }

            Assert.NotEmpty(links);
        }
    }
}
