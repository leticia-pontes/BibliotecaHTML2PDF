namespace htmltopdf
{
    public class PdfGenerator
    {
        public void CreatePdf(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    // PDF Header
                    writer.WriteLine("%PDF-1.4");

                    // Create first object (Catalog)
                    writer.WriteLine("1 0 obj");
                    writer.WriteLine("<< /Type /Catalog /Pages 2 0 R >>");
                    writer.WriteLine("endobj");

                    // Create second object (Pages)
                    writer.WriteLine("2 0 obj");
                    writer.WriteLine("<< /Type /Pages /Kids [3 0 R] /Count 1 >>");
                    writer.WriteLine("endobj");

                    // Create third object (Page)
                    writer.WriteLine("3 0 obj");
                    writer.WriteLine("<< /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] /Contents 4 0 R >>");
                    writer.WriteLine("endobj");

                    // Create fourth object (Content)
                    writer.WriteLine("4 0 obj");
                    writer.WriteLine("<< /Length 55 >>");
                    writer.WriteLine("stream");
                    writer.WriteLine("BT /F1 24 Tf 100 700 Td (Descobri que da para fazer essa porra assim!) Tj ET");
                    writer.WriteLine("endstream");
                    writer.WriteLine("endobj");

                    // Cross-reference table
                    writer.WriteLine("xref");
                    writer.WriteLine("0 5");
                    writer.WriteLine("0000000000 65535 f ");
                    writer.WriteLine("0000000010 00000 n ");
                    writer.WriteLine("0000000067 00000 n ");
                    writer.WriteLine("0000000120 00000 n ");
                    writer.WriteLine("0000000179 00000 n ");

                    // Trailer
                    writer.WriteLine("trailer");
                    writer.WriteLine("<< /Size 5 /Root 1 0 R >>");
                    writer.WriteLine("startxref");
                    writer.WriteLine("256");
                    writer.WriteLine("%%EOF");
                }
            }
        }
    }

    public class Test
    {
        public static void Main(string[] args)
        {
            // Specify the file path on C: drive
            string filePath = @"C:\Users\letic\Documents\SimpleGeneratedPDF.pdf";

            // Create an instance of PdfGenerator
            PdfGenerator pdfGenerator = new PdfGenerator();

            try
            {
                // Call the CreatePdf method to generate the PDF
                pdfGenerator.CreatePdf(filePath);
                Console.WriteLine($"PDF criado em {filePath}");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("ERRO: deu bucha, não tem permissão para executar");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO: deu bucha e não sei nem te dizer qual foi o b.o que deu kk: {ex.Message}");
            }
        }
    }
}
