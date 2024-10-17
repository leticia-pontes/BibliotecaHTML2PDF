using AngleSharp.Css.Dom;
using AngleSharp.Css.Parser;
using System.IO;

namespace HtmlToPdfConverter.Parsers
{
    public class CssParser
    {
        public string LoadCss(string cssFilePath)
        {
            // Read the CSS file content
            return File.ReadAllText(cssFilePath);
        }

        public ICssStyleSheet ParseCss(string cssContent)
        {
            var cssParser = new AngleSharp.Css.Parser.CssParser(); // Use the AngleSharp CssParser
            return cssParser.ParseStyleSheet(cssContent);
            //return cssParser.Parse(cssContent); // Parse the CSS content
        }
    }
}
