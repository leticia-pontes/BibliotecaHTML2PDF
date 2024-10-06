using AngleSharp;
using AngleSharp.Dom;
using HtmlToPdfConverter.Parsers; // Ensure this is included
using System.Threading.Tasks;

namespace HtmlToPdfConverter.Processors
{
    public class CssApplier
    {
        public async Task<IDocument> ApplyCssToHtmlAsync(string htmlContent, string cssContent)
        {
            // Create a new browsing context with CSS support
            var config = Configuration.Default.WithCss();
            var context = BrowsingContext.New(config);

            // Load the HTML content into an AngleSharp document
            var document = await context.OpenAsync(req => req.Content(htmlContent));

            // Load CSS styles using the custom CssParser
            var cssParser = new CssParser();
            var stylesheet = cssParser.ParseCss(cssContent); // Parse the CSS content

            // Create a style element
            var styleElement = document.CreateElement("style");
            styleElement.TextContent = stylesheet.ToCss(); // Convert to CSS string

            // Append the style element to the document head
            document.Head.AppendChild(styleElement);

            // Return the styled document
            return document;
        }
    }
}
