using HtmlToPdfConverter.Parsers;
using HtmlToPdfConverter.Processors;
using System.Threading.Tasks;

namespace HtmlToPdfConverter.Services
{
    public class HtmlToPdfConverterService
    {
        private readonly HtmlParser _htmlParser;
        private readonly CssParser _cssParser;
        private readonly CssApplier _cssApplier;
        private readonly PdfGenerator _pdfGenerator;

        public HtmlToPdfConverterService()
        {
            _htmlParser = new HtmlParser();
            _cssParser = new CssParser();
            _cssApplier = new CssApplier();
            _pdfGenerator = new PdfGenerator();
        }

        public async Task ConvertAsync(string htmlFilePath, string cssContent, string outputPdfPath)
        {
            // Read HTML
            var htmlDoc = _htmlParser.LoadHtml(htmlFilePath);
            var htmlContent = htmlDoc.DocumentNode.OuterHtml;

            // Apply CSS to HTML
            var styledDocument = await _cssApplier.ApplyCssToHtmlAsync(htmlContent, cssContent);

            // Generate the PDF
            _pdfGenerator.ConvertHtmlToPdf(outputPdfPath, styledDocument);
        }
    }
}
