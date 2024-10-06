using AngleSharp.Dom;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Threading.Tasks;

namespace HtmlToPdfConverter.Processors
{
    public class PdfGenerator
    {
        public void ConvertHtmlToPdf(string outputPdfPath, IDocument styledDocument)
        {
            QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.Content().Element(ComposeStyledDocument(styledDocument));
                });
            }).GeneratePdf(outputPdfPath);
        }

        private Action<IContainer> ComposeStyledDocument(IDocument document)
        {
            return container =>
            {
                container.Column(column =>
                {
                    foreach (var element in document.All)
                    {
                        switch (element)
                        {
                            case AngleSharp.Html.Dom.IHtmlHeadingElement heading when heading.LocalName == "h1":
                                column.Item().Text(heading.TextContent).FontSize(24).Bold();
                                break;
                            case AngleSharp.Html.Dom.IHtmlHeadingElement heading when heading.LocalName == "h2":
                                column.Item().Text(heading.TextContent).FontSize(20).Bold();
                                break;
                            case AngleSharp.Html.Dom.IHtmlParagraphElement paragraph:
                                column.Item().Text(paragraph.TextContent);
                                break;
                            case AngleSharp.Dom.IElement ul when ul.LocalName == "ul":
                                column.Item().Column(subColumn =>
                                {
                                    foreach (var li in ul.Children)
                                    {
                                        if (li.LocalName == "li")
                                        {
                                            subColumn.Item().Text(li.TextContent);
                                        }
                                    }
                                });
                                break;
                            case AngleSharp.Html.Dom.IHtmlImageElement img:
                                column.Item().Image(img.Source); // Assuming the src is a valid URL
                                break;
                            default:
                                break; // Default case
                        }
                    }
                });
            };
        }
    }
}
