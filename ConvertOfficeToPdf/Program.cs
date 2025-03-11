namespace ConvertOfficeToPdf
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var url = "http://127.0.0.1:8099/report2pdf";

                Console.WriteLine("Введите путь до исходного файла");
                var filePath = Console.ReadLine();
                filePath = Path.GetFullPath(filePath!);

                using (var client = new HttpClient())
                using (var form = new MultipartFormDataContent())
                {
                    client.DefaultRequestHeaders.Clear();
                    form.Add(new ByteArrayContent(File.ReadAllBytes(filePath)), "file", filePath);

                    var response = await client.PostAsync(url, form);
                    var responseBytes = await response.Content.ReadAsByteArrayAsync();

                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileNameWithoutExtension(filePath)) + ".pdf";

                    await File.WriteAllBytesAsync(savePath, responseBytes);

                    Console.WriteLine("PDF saved to " + savePath);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            
        }
    }
}
