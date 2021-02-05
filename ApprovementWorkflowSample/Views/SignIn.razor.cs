using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ApprovementWorkflowSample.Views
{
    public partial class SignIn
    {
        [Inject]
        public IJSRuntime? JSRuntime { get; set; }
        [Parameter]
        public string Email { get; set; } = "";
        [Parameter]
        public string Password { get; set; } = "";
        public async Task StartSigningIn()
        {
            System.Console.WriteLine(Email);
            if(string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(Password))
            {
                await JSRuntime!.InvokeAsync<object>("Alert","Email and Password are required");
            }
            System.Console.WriteLine("OK");
        }
    }
}