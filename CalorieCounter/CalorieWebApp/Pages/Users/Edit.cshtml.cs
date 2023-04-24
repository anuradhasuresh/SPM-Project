using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using CalorieCounterAPI.Controllers;
using CalorieCounterAPI.Interfaces;
using CalorieCounterAPI.Models;
using CalorieCounterAPI.Repositories;
using CalorieCounterAPI.Data;
using CalorieCounterAPI;

/// <summary>
/// Class to provide edit functionality to the Edit User Page
/// </summary>

namespace CalorieWebApp.Pages.Users
{
    public class EditModel : PageModel
    {
        public CalorieClass user = new();
        public string errorMessage = "";
        public string successMessage = "";
        /// <summary>
        /// Performs a HTTP Get Call
        /// </summary>
        public async void OnGet()
        {
            string id = Request.Query["id"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5215");
                //HTTP GET
                var responseTask = client.GetAsync("Calorie/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<CalorieClass>(readTask);
                }
            }
        }
        /// <summary>
        /// Performs a HTTP Post Call
        /// </summary>
        public async Task<IActionResult> OnPost()
        {
            user.Id = int.Parse(Request.Form["id"]);
            user.Name = Request.Form["name"];
            user.Age = int.Parse(Request.Form["age"]);
            user.Gender = Request.Form["gender"];
            user.Height = int.Parse(Request.Form["height"]);
            user.Weight = int.Parse(Request.Form["weight"]);
            user.CurrentCalorieIntake = int.Parse(Request.Form["currentCalorieIntake"]);

            if (user.Name.Length == 0 || user.Age == 0 || user.Gender == ""|| user.CurrentCalorieIntake == 0 || user.Height == 0 || user.Weight == 0)
                errorMessage = "Please enter your details";
            else
            {
                var opt = new JsonSerializerOptions() { WriteIndented = true };
                string json = System.Text.Json.JsonSerializer.Serialize<CalorieClass>(user, opt);
                Console.WriteLine(json);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5215");

                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    var result = await client.PutAsync("Calorie", content);
                    string resultContent = await result.Content.ReadAsStringAsync();
                    Console.WriteLine(resultContent);

                    if (!result.IsSuccessStatusCode)
                        errorMessage = "Error editing";
                    else
                        successMessage = "Successfully edited";
                }
            }
            return RedirectToPage("/Users/Index");
        }
    }
}