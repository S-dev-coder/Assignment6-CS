using System.Text;

namespace ConsoleApp6
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Prompt for URL input
            Console.WriteLine("Enter the URL to read content from:");
            string url = Console.ReadLine();

       
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                Console.WriteLine("Invalid URL format. Please try again.");
                return;
            }

            try
            {
                // Create HttpClient instance and send asynchronous request
                using (var httpClient = new HttpClient())
                {
                    Console.WriteLine("Downloading content...");
                    var response = await httpClient.GetAsync(url);

                    // Check for successful response
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Failed to download content. Status code: {response.StatusCode}");
                        return;
                    }

                    
                    string content = await response.Content.ReadAsStringAsync();

                  
                    using (var fileStream = File.OpenWrite("temp.txt"))
                    {
                        Console.WriteLine("Writing content to file...");
                        await fileStream.WriteAsync(Encoding.UTF8.GetBytes(content));
                    }

                    Console.WriteLine("Content downloaded and written to file temp.txt successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}


