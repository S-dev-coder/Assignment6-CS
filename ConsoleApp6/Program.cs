class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the URL to read:");
        string url = Console.ReadLine();
        var t = ReadMainContentAsync(url);
        Console.WriteLine("Asynchronously call done. Press any key to terminate the main program.");
        Console.ReadKey(); // Prevent immediate termination of main thread
    }
    static async Task ReadMainContentAsync(string url)
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string htmlContent = await response.Content.ReadAsStringAsync();
                await File.WriteAllTextAsync("A.txt", htmlContent);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading URL: {ex.Message}");
        }
    }
}