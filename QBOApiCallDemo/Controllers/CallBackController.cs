using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
namespace QBOApiCallDemo.Controllers
{
    public class CallBackController : Controller
    {
        /// <summary>
        /// Code and realmid/company id recieved on Index page after redirect is complete from Authorization url
        /// </summary>
  
        public async Task<ActionResult> Index()
        {
            //Sync the state info and update if it is not the same
            var state = HttpContext.Request.Query["state"].ToString();
            if (state.Equals(HomeController.auth2Client.CSRFToken, StringComparison.Ordinal))
            {
                ViewBag.State = state + " (valid)";
            }
            else
            {
                ViewBag.State = state + " (invalid)";
            }

            string code = HttpContext.Request.Query["code"].ToString() ?? "none";
            string realmId = HttpContext.Request.Query["realmId"].ToString() ?? "none";
            await GetAuthTokensAsync(code, realmId);

            ViewBag.Error = HttpContext.Request.Query["error"].ToString() ?? "none";

            return RedirectToAction("Tokens", "Home");
        }

        /// <summary>
        /// Exchange Auth code with Auth Access and Refresh tokens and add them to Claim list
        /// </summary>
        private async Task GetAuthTokensAsync(string code, string realmId)
        {
            #region original Code
            if (realmId != null)
            {
                HttpContext.Session.SetString("realmId", realmId);
            }

            //Request.GetOwinContext().Authentication.SignOut("TempState");
            var tokenResponse = await HomeController.auth2Client.GetBearerTokenAsync(code);
            if (tokenResponse.AccessToken != null)
            {
                //var options = new CookieOptions
                //{
                //    Expires = new DateTime(tokenResponse.AccessTokenExpiresIn) // Set the desired expiration date
                //};
                Response.Cookies.Append("AccessTokenResponse", tokenResponse.AccessToken);

              

                //Response.Cookies.Append("AccessTokenResponse", tokenResponse.AccessToken);

                //// Set a cookie with additional options
                //var cookieOptions = new CookieOptions
                //{
                //    Expires = DateTime.Now.AddMinutes(60),
                //    HttpOnly = true,
                //    Secure = true
                //};
                //Response.Cookies.Append("AccessTokenResponse", tokenResponse.AccessToken, cookieOptions);

            }
            var claims = new List<Claim>();

            if (HttpContext.Session.GetString("realmId") != null)
            {
                claims.Add(new Claim("realmId", HttpContext.Session.GetString("realmId").ToString()));
            }

            if (!string.IsNullOrWhiteSpace(tokenResponse.AccessToken))
            {
                claims.Add(new Claim("access_token", tokenResponse.AccessToken));
                claims.Add(new Claim("access_token_expires_at", (DateTime.Now.AddSeconds(tokenResponse.AccessTokenExpiresIn)).ToString()));
            }

            if (!string.IsNullOrWhiteSpace(tokenResponse.RefreshToken))
            {
                claims.Add(new Claim("refresh_token", tokenResponse.RefreshToken));
                claims.Add(new Claim("refresh_token_expires_at", (DateTime.Now.AddSeconds(tokenResponse.RefreshTokenExpiresIn)).ToString()));
            }

            var identity = new ClaimsIdentity(claims, "Cookies");

            // Sign in the user with the ClaimsIdentity
            await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(identity));

            #endregion

        }
    }
}
