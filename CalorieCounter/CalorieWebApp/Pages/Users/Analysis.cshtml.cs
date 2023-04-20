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
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CalorieWebApp.Pages.Users;

/// <summary>
/// Class to provide analysis to the Analysis Page
/// </summary>
public class AnalysisModel : PageModel
{
    public CalorieClass user = new();
    public Analysis analysis = new();
    
    public string errorMessage = "";
    /// <summary>
    /// Performs a HTTP Get call
    /// </summary>
    public async void OnPost()
    {
        user.Name = Request.Form["name"];

        if (user.Name.Length == 0)
            errorMessage = "Please enter a name";
        else
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5215");
                //HTTP GET
                var responseTask = client.GetAsync("Analysis/" + user.Name);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    analysis = JsonConvert.DeserializeObject<Analysis>(readTask);
                }
                else
                    errorMessage = "Error in fetching analysis";
            }
        }
    }
}

