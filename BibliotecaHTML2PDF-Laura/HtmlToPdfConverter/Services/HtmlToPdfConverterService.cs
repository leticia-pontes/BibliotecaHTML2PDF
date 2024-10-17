using HtmlToPdfConverter.Parsers;
using HtmlToPdfConverter.Processors;
using System.IO;
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

        // Método modificado para aceitar conteúdo HTML como string
        public async Task ConvertAsync(string htmlContent, string cssContent, Stream outputStream)
        {
            // Aplica CSS ao HTML
            var styledDocument = await _cssApplier.ApplyCssToHtmlAsync(htmlContent, cssContent);

            // Gera o PDF no stream de saída
            using (var memoryStream = new MemoryStream())
            {
                // Chama o método de conversão, adaptado para aceitar um Stream
                _pdfGenerator.ConvertHtmlToPdf(memoryStream, styledDocument);

                // Reseta a posição do stream antes de retornar
                memoryStream.Position = 0;

                // Copia o conteúdo do memoryStream para o outputStream
                await memoryStream.CopyToAsync(outputStream);
            }
        }
    }
}
