using System;
using System.IO;
using System.Threading.Tasks;
using HtmlToPdfConverter.Services;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var converter = new HtmlToPdfConverterService();

            // Define file paths correctly relative to the current working directory
            string htmlFilePath = Path.Combine("..", "..", "Assets", "html", "input.html");  // Path to your HTML file
            string cssFilePath = Path.Combine("..", "..", "Assets", "css", "styles.css");    // Path to your CSS file
            string outputPdfPath = Path.Combine("..", "..", "Assets", "output.pdf");         // Path to save the output PDF

            // Print paths for debugging
            Console.WriteLine($"HTML File Path: {htmlFilePath}");
            Console.WriteLine($"CSS File Path: {cssFilePath}");
            Console.WriteLine($"Output PDF Path: {outputPdfPath}");

            // Check if HTML and CSS files exist
            if (!File.Exists(htmlFilePath))
            {
                throw new FileNotFoundException($"HTML file not found at path: {htmlFilePath}");
            }
            if (!File.Exists(cssFilePath))
            {
                throw new FileNotFoundException($"CSS file not found at path: {cssFilePath}");
            }

            // Read CSS content from file
            string cssContent = await File.ReadAllTextAsync(cssFilePath);

            // Call the conversion method with the CSS content as a string
            await converter.ConvertAsync(htmlFilePath, cssContent, outputPdfPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
