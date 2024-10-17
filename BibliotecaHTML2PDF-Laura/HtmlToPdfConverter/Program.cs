using System;
using System.IO;
using System.Threading.Tasks;
using HtmlToPdfConverter.Services;
using QuestPDF.Infrastructure;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            // Definindo a licença do QuestPDF como Community
            QuestPDF.Settings.License = LicenseType.Community;

            var converter = new HtmlToPdfConverterService();

            // Define os caminhos dos arquivos de forma relativa ao diretório atual
            string htmlFilePath = Path.Combine("Assets", "html", "input.html");  // Caminho para o arquivo HTML
            string cssFilePath = Path.Combine("Assets", "css", "styles.css");    // Caminho para o arquivo CSS

            // Imprime os caminhos para depuração
            Console.WriteLine($"HTML File Path: {htmlFilePath}");
            Console.WriteLine($"CSS File Path: {cssFilePath}");

            // Verifica se os arquivos HTML e CSS existem
            if (!File.Exists(htmlFilePath))
            {
                throw new FileNotFoundException($"Arquivo HTML não encontrado no caminho: {htmlFilePath}");
            }
            if (!File.Exists(cssFilePath))
            {
                throw new FileNotFoundException($"Arquivo CSS não encontrado no caminho: {cssFilePath}");
            }

            // Lê o conteúdo do CSS do arquivo
            string cssContent = await File.ReadAllTextAsync(cssFilePath);

            // Cria um MemoryStream para o PDF
            using (var outputPdfStream = new MemoryStream())
            {
                // Chama o método de conversão com o conteúdo do CSS como string
                await converter.ConvertAsync(htmlFilePath, cssContent, outputPdfStream);

                // Aqui você pode solicitar ao usuário onde salvar o PDF
                Console.Write("Digite o caminho para salvar o PDF de saída (ou pressione Enter para usar o padrão 'Assets/output.pdf'): ");
                string outputPdfPath = Console.ReadLine();
                if (string.IsNullOrEmpty(outputPdfPath))
                {
                    outputPdfPath = Path.Combine("Assets", "output.pdf"); // Caminho padrão
                }

                // Salva o conteúdo do MemoryStream no arquivo de saída
                using (var fileStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
                {
                    outputPdfStream.Position = 0; // Reseta a posição do MemoryStream antes de copiar
                    await outputPdfStream.CopyToAsync(fileStream);
                }

                Console.WriteLine($"PDF gerado com sucesso em: {outputPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro: {ex.Message}");
        }
    }
}
