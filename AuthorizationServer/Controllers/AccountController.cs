using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AuthorizationServer.Controllers {
    public class AccountController : Controller {
        // GET: Account
        public ActionResult Login() {
            var authentication = HttpContext.GetOwinContext().Authentication;
            if (Request.HttpMethod == "POST") {
                var isPersistent = !string.IsNullOrEmpty(Request.Form.Get("isPersistent"));

                if (!string.IsNullOrEmpty(Request.Form.Get("submit.Signin"))) {
                    authentication.SignIn(
                        new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = isPersistent },
                        new System.Security.Claims.ClaimsIdentity(new[] { new Claim(ClaimsIdentity.DefaultNameClaimType, Request.Form["username"]) }, "Application"));
                }
            }
            return View();
        }
        public ActionResult Logout() => View();
    }
}