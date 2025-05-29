using HtmlAgilityPack;
using MMAPredictor.DataScrapper.Interface;
using MMAPredictor.DataScrapper.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPredictor.DataScrapper.Service
{
    public class UFCFighterLinksScrapperService : IUFCFighterLinksScrapperService
    {
        public async IAsyncEnumerable<Uri> ExtractFighterLinksStartingWithLetter(char letter, string? linksPageTemplate, string? pagePath=null)
        {
            HtmlDocument? htmlDoc = null;
            try
            {
                if (!string.IsNullOrEmpty(pagePath))
                {
                    htmlDoc = new HtmlDocument();
                    htmlDoc.Load(pagePath);
                }
                else if (!string.IsNullOrEmpty(linksPageTemplate) && linksPageTemplate.Contains("{0}"))
                {
                    htmlDoc = await new HtmlWeb().LoadFromWebAsync(string.Format(linksPageTemplate, letter));
                }
            }
            catch
            {
                yield break;
            }

            if (htmlDoc is null)
            {
                yield break;
            }

            IEnumerable<string> nodeCollection = ScrappingUtils.SelectListNodes(htmlDoc, "//body//tbody/tr[@class='b-statistics__table-row']/td[@class='b-statistics__table-col'][1]/a", "href");
            foreach(var node in nodeCollection)
            {
                yield return new Uri(node);
            }
        }
    }
}
