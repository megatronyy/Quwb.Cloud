using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Website.App.Common
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var auth = HttpContext.User;
            if (auth != null && auth.Identities.Count() > 0)
            {
                foreach (var identi in auth.Identities) {
                    foreach (var claim in identi.Claims) {
                        if (claim.Type == ClaimTypes.UserData) {
                            ViewBag.UserAccount = claim.Value;
                        }
                    }
                }
                ViewBag.UserAccount = auth;
            }
            base.OnActionExecuting(context);
        }
    }
}