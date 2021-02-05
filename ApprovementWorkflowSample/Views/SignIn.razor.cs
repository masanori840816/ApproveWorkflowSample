using System.IO;
using System.Text;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApprovementWorkflowSample.Applications.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace ApprovementWorkflowSample.Views
{
    public partial class SignIn
    {
        [Inject]
        public IJSRuntime? JSRuntime { get; init; }
        [Inject]
        public IHttpClientFactory? HttpClients { get; init; }
        [Inject]
        public IConfiguration? Configuration { get; init; }
        [Inject]
        public NavigationManager? Navigation { get; init; }
        [Parameter]
        public string Email { get; set; } = "";
        [Parameter]
        public string Password { get; set; } = "";
        [Parameter]
        public string AdditionalClassName { get; set; } = "";
        public async Task StartSigningIn()
        {
            if(string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(Password))
            {
                AdditionalClassName = "login_failed";
                await JSRuntime!.InvokeAsync<object>("Page.showAlert","Email and Password are required");
                return;
            }
            var httpClient = HttpClients.CreateClient();
            var signInValue = new SignInValue(Email, Password);
            var context = new StringContent(JsonConvert.SerializeObject(signInValue), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(Path.Combine(Configuration!["BaseUrl"], "Users/SignIn"), context);
            if(response.IsSuccessStatusCode == false)
            {
                await JSRuntime!.InvokeAsync<object>("Page.showAlert","Failed access");
                return;
            }
            string resultText = await response.Content.ReadAsStringAsync();
            bool.TryParse(resultText, out var result);
            if(result)
            {
                Navigation!.NavigateTo("https://www.bing.com/");
                return;
            }
            AdditionalClassName = "login_failed";
            await JSRuntime!.InvokeAsync<object>("Page.showAlert","Email or Password are not match");
        }
    }
}