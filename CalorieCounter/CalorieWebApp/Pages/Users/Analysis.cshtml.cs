using System;
namespace CalorieWebApp.Pages.Users
{
	public class Analysis
	{
		// public Analysis()
		// {
		// }
		public List<Users> Calorie = new();
        public async void OnGet()
        {
            using (var client = new HttpClient())
            {
				client.BaseAddress = new Uri("https://localhost:5215");
				var responseTask = client.GetAsync("Analysis/name");

                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    Calorie = JsonConvert.DeserializeObject<List<Users>>(readTask);

                }
            }
        }

	}
}

