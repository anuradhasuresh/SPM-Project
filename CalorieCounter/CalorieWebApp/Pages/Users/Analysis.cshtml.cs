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
    public string DataAnalysis = "";
    public string errorMessage = "";
    public string successMessage = "";
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
                    var DataAnalysis = await result.Content.ReadAsStringAsync();
                    //DataAnalysis = JsonConvert.DeserializeObject<String>(readTask);
                    //DataAnalysis = readTask;
                    //Console.WriteLine(DataAnalysis);
                    //DataAnalysis.Replace(".",".</p><p>");
                    //successMessage = "Successfully fetched analysis";
                    //Console.WriteLine("Modified Data Analysis");
                    //Console.WriteLine(DataAnalysis);
                    //RedirectToPage("/Users/Index");
                }
                else
                    errorMessage = "Error in fetching analysis";
            }
        }
    }
}

