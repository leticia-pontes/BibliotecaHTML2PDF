using HtmlAgilityPack;

namespace HtmlToPdfConverter.Parsers
{
    public class HtmlParser
    {
        public HtmlDocument LoadHtml(string htmlFilePath)
        {
            HtmlDocument document = new HtmlDocument();
            document.Load(htmlFilePath);
            return document;
        }
    }
}
