using Microsoft.AspNetCore.Hosting;
using Netwise.Interfaces;
using Netwise.Models;
using System.Text;
using System.Text.Json;

namespace Netwise.Services
{
    public class FileService : IFileService
    {
        private readonly HttpClient _httpClient;
        private readonly string _filePath;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(HttpClient httpClient, IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = httpClient;
            _filePath = "wwwroot/file/example.txt";
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> OpenFile()
        {
            if (!File.Exists(_filePath))
            {
                throw new Exception(message: "Plik nie istnieje. Pobierz pierwsze dane.");
            }
            else
            {
                using StreamReader sr = new StreamReader(_filePath);
                return await sr.ReadToEndAsync();
            }
        }

        public async Task<List<CatFact>> ReadFile()
        {
            if (!File.Exists(_filePath))
            {
                throw new Exception(message: "Plik nie istnieje. Pobierz pierwsze dane.");
            }

            List<CatFact> catFacts = [];

            using (StreamReader sr = new StreamReader(_filePath))
            {
                string line;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    CatFact catFact = JsonSerializer.Deserialize<CatFact>(line);
                    catFacts.Add(catFact);
                }
            }

            if (catFacts.Count == 0)
            {
                throw new Exception(message: "Brak zapisanych danych w pliku tekstowym.");
            }

            return catFacts;
        }

        public async Task WriteToFile()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://catfact.ninja/fact");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                using StreamWriter sw = File.AppendText(_filePath);
                await sw.WriteLineAsync(content);
            }
            else
            {
                throw new HttpRequestException($"Brak odpowiedzi serwera.");
            }
        }
    }
}
