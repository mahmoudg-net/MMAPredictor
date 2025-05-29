using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMAPredictor.DataScrapper.Utilities
{
    public class ScrappingUtils
    {
        public static T? SelectNode<T>(HtmlDocument htmlDoc, string xPath) where T : class
        {
            var node = htmlDoc.DocumentNode.SelectSingleNode(xPath);
            string? text = null;
            if (node != null)
            {
                text = node.InnerText?.Trim('\n', ' ');
                return text as T;
            }
            return null;
        }

        public static IEnumerable<string> SelectListNodes(HtmlDocument htmlDoc, string xPath, string? attributeName=null)
        {
            HtmlNodeCollection? nodeList = htmlDoc.DocumentNode.SelectNodes(xPath);
            if (nodeList != null)
            {
                foreach (var node in nodeList)
                {
                    if (!string.IsNullOrEmpty(attributeName))
                    {
                        yield return node.Attributes[attributeName].Value;
                    }
                    else
                    {
                        yield return node.InnerText.Trim();
                    }
                }
            }
        }

        public static async Task<bool> SaveUrlToFile(string url, string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    return true;
                }

                HtmlWeb web = new HtmlWeb();
                var htmlDoc = await web.LoadFromWebAsync(url);
                using Stream streamWriter = File.OpenWrite(path);
                htmlDoc.Save(streamWriter);

                if (!File.Exists(path))
                {
                    return false;
                }
                return new FileInfo(path).Length > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
