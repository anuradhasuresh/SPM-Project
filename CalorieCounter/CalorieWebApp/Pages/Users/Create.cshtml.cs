using System;
using System.Text.Json;

namespace CalorieWebApp.Pages.Users
{
	public class Create
	{
		public Todo todo = new();
		public string errorMessage = "";
		public string successMessage = "";

		public async void onpost()
		{
			todo.Description = Request.Form["description"];

			if(todo.Description.length == 0)
			{
				errorMessage = "Description is required";
			}
			else
			{
				var opt = new JsonSerializerOptions() { WriteIndented = true };
				string json = System.Text.Json.JsonSerializer.Serialize<todo>(todo, opt);

				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri("http://localhost:5240");
					var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

					var result = await client.PostAsync("todo", content);
					string resultcontent = await result.Content.ReadAsStringAsync();
					Console.WriteLine(resultContent);

					if (!result.IsSuccessStatusCode)
					{
						errorMessage = "Error adding";

					}
					else
					{
						successMessage = "successfully added";
					}
				}
			}
		}
	}
}

