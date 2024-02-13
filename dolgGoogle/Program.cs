using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;

class Program
{
    static async System.Threading.Tasks.Task Main(string[] args)
    {
        Console.WriteLine("Введите текст для поиска:");
        string query = Console.ReadLine();

        // Ваш API-ключ для Google Custom Search JSON API
        string apiKey = "AIzaSyAZz23xk34vPEWbUOT-5mYO2qbzPlVu_k8";

        // Идентификатор поискового движка Google Custom Search
        string searchEngineId = "56aa4a0f4d49b4c9c";

        // Номер страницы (начиная с 1)
        int startPage = 1;

        // Количество результатов на странице
        int numPerPage = 10;

        // Параметр start определяет с какого элемента начать выводить результаты
        int start = (startPage - 1) * numPerPage + 1;

        // Формируем URL для запроса
        string url = $"https://www.googleapis.com/customsearch/v1?key={apiKey}&cx={searchEngineId}&q={Uri.EscapeDataString(query)}&start={start}";

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();

            // Обработка и вывод результатов
            JObject jsonResponse = JObject.Parse(responseBody);
            JArray items = (JArray)jsonResponse["items"];

            if (items != null && items.Count > 0)
            {
                Console.WriteLine("Результаты поиска (первая страница):");
                foreach (var item in items)
                {
                    Console.WriteLine(item["title"]);
                    Console.WriteLine(item["link"]);
                    Console.WriteLine("-----");
                }
            }
            else
            {
                Console.WriteLine("Результаты не найдены на первой странице.");
            }
        }
    }
}
