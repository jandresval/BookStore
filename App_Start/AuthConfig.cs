using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using BookStore.Models;

namespace BookStore
{
    public static class AuthConfig
    {
        /// <summary>
        /// Define clients to authenticate
        /// </summary>
        public static void RegisterAuth()
        {
            //Those are the information to connect to facebook.

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "1426266977618661",
                appSecret: "2495dc886a8a279a7f109d85ec1efe17");

        }
    }
}
