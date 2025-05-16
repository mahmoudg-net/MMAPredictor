using MMAPredictor.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MMAPredictor.DataScrapper
{
    public class UFCScrapperService : IUFCScrapperService
    {
        public async Task<FighterDTO?> ScrapFighterPageAsync(string url)
        {
            try
            {
                HtmlWeb web = new HtmlWeb();                
                var htmlDoc = await web.LoadFromWebAsync(url);
                var fighterNameNode = htmlDoc.DocumentNode.SelectSingleNode("//body/section[@class='b-statistics__section_details']//span[@class='b-content__title-highlight']");
                if (fighterNameNode != null)
                {
                    return new FighterDTO() { Name = fighterNameNode.InnerText?.Trim('\n',' ') };
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
