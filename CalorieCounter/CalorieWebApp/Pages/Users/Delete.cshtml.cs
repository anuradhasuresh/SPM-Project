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
    public class DeleteModel : PageModel
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
        public async void OnPost()
        {

            string id = Request.Query["id"];

            using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5215");

                    var result = await client.DeleteAsync("Calorie/"+ id);

                    if (!result.IsSuccessStatusCode)
                        errorMessage = "Error editing";
                    else
                    {
                        successMessage = "Successfully deleted";
                    }


                }
       
        }
    }
}