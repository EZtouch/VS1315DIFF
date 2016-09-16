﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace VS1315DIFF.Account
{
    public partial class OpenAuthProviders : UserControl
    {
        public string ReturnUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                var provider = Request.Form["provider"];
                if (provider == null)
                    return;
                // Request a redirect to the external login provider
                var redirectUrl =
                    ResolveUrl(string.Format(CultureInfo.InvariantCulture,
                        "~/Account/RegisterExternalLogin?{0}={1}&returnUrl={2}", IdentityHelper.ProviderNameKey,
                        provider, ReturnUrl));
                var properties = new AuthenticationProperties {RedirectUri = redirectUrl};
                // Add xsrf verification when linking accounts
                if (Context.User.Identity.IsAuthenticated)
                    properties.Dictionary[IdentityHelper.XsrfKey] = Context.User.Identity.GetUserId();
                Context.GetOwinContext().Authentication.Challenge(properties, provider);
                Response.StatusCode = 401;
                Response.End();
            }
        }

        public IEnumerable<string> GetProviderNames()
        {
            return
                Context.GetOwinContext()
                    .Authentication.GetExternalAuthenticationTypes()
                    .Select(t => t.AuthenticationType);
        }
    }
}