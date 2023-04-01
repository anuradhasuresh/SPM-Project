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
/// Class to provide functionality to display all records in the Index Page
/// </summary>
public class IndexModel : PageModel
{
    public List<CalorieClass> Users = new();
    /// <summary>
    /// Performs a HTTP Get Call
    /// </summary>
    public async void OnGet()
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://localhost:5215"); 
            //HTTP GET
            var responseTask = client.GetAsync("Calorie/GetAllItems");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = await result.Content.ReadAsStringAsync();
                Users = JsonConvert.DeserializeObject<List<CalorieClass>>(readTask);
            }
        }
    }
}