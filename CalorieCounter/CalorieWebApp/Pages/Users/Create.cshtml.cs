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

namespace CalorieWebApp.Pages.Users;
/// <summary>
/// Class to provide create functionality to the Create User Page
/// </summary>
public class CreateModel : PageModel
{
    public CalorieClass user = new();
    public string errorMessage = "";
    public string successMessage = "";
    /// <summary>
    /// Performs HTTP Post
    /// </summary>
    public async void OnPost()
    {
        user.Id = int.Parse(Request.Form["id"]);
        user.Name = Request.Form["name"];
        user.Age = int.Parse(Request.Form["age"]);
        user.Gender = Request.Form["gender"];
        user.CurrentCalorieIntake = int.Parse(Request.Form["currentCalorieIntake"]);

        if (user.Name.Length == 0 || user.Age == 0 || user.Gender.Length == 0 || user.CurrentCalorieIntake == 0)
            errorMessage = "Please enter your details";
        else
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string json = System.Text.Json.JsonSerializer.Serialize<CalorieClass>(user, opt);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5215");

                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var result = await client.PostAsync("Calorie", content);
                string resultContent = await result.Content.ReadAsStringAsync();

                Console.WriteLine(resultContent);

                if (!result.IsSuccessStatusCode)
                    errorMessage = "Error adding";
                else
                    successMessage = "Successfully added";
            }
        }
    }
}

