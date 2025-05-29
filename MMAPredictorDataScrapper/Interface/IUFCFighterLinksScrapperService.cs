using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPredictor.DataScrapper.Interface
{
    public interface IUFCFighterLinksScrapperService
    {
        IAsyncEnumerable<Uri> ExtractFighterLinksStartingWithLetter(char letter, string? linksPageTemplate, string? pagePath = null);
    }
}
