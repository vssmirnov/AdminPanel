using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly AuthService _authService;
        public CustomAuthenticationStateProvider(AuthService authService)
        {
            _authService = authService;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            /* var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "username"),
                }, "Fake authentication type");
 */
            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            var userFromService = await _authService.LoginAsync(username, password);

            if (userFromService == null)
            {
                return false;
            }
            else
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                }, "Fake authentication type");

                var user = new ClaimsPrincipal(identity);

                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

                return true;
            }

        }

        public void Logout()
        {
            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}