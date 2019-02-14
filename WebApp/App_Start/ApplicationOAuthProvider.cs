using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using MySql.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApp.Models;
using Microsoft.AspNet.Identity;
using DataAccesLayer;
using DataAccesLayer.UnitOfWork;
using System.Web.Routing;

namespace WebApp.App_Start
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            var dbContext = context.OwinContext.Get<ApplicationDbContext>();
            if (dbContext.GetUserCount()<= 0)
            {
                UOWUserProfile dbcontext = new UOWUserProfile();
                IdentityResult result = null;
                var userModel = new Models.ApplicationUser { Email = "3reksa@gmail.com", UserName = "3reksa@gmail.com" };
                try
                {
                    Random rand = new Random();
                    var password = Helper.GetRandomAlphanumericString(6) + "3#";
                    result = await userManager.CreateAsync(userModel, password);
                    if (result.Succeeded)
                    {
                        string code = await userManager.GenerateEmailConfirmationTokenAsync(userModel.Id);
                        System.Web.Mvc.UrlHelper urlHelper = new System.Web.Mvc.UrlHelper(HttpContext.Current.Request.RequestContext, RouteTable.Routes);
                        string callbackUrl = urlHelper.Action(
                            "ConfirmEmail",
                            "Account",
                            new { userId = userModel.Id, code = code },
                            HttpContext.Current.Request.Url.Scheme
                            );

                        await userManager.SendEmailAsync(userModel.Id, "Confirm your account", "Your Password : " + password + " , and Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                        var RoleManager = context.OwinContext.GetUserManager<ApplicationRoleManager>();
                        var role = "Admin";
                        if (!await RoleManager.RoleExistsAsync(role))
                        {
                            var roleCreate = RoleManager.Create(new IdentityRole(Guid.NewGuid().ToString(), role));
                            if (!roleCreate.Succeeded)
                                throw new SystemException("User Tidak Berhasil Ditambah");
                        }
                        var addUserRole = await userManager.AddToRoleAsync(userModel.Id, role);

                        if (!addUserRole.Succeeded)
                        {
                            throw new SystemException("User Tidak Berhasil Ditambah");
                        }

                        var usesr = dbcontext.AddNewUser(new DataAccesLayer.Models.petugas { Email= "3reksa@gmail.com", Name="Administrator", Role=role, UserId = userModel.Id });
                       
                    }
                    throw new SystemException("User Tidak Berhasil Ditambah");

                }
                catch (Exception ex)
                {
                    context.SetError("App Not Yet Ready", "if you administrator please verify your account."+ex.Message);
                    return;
                }
            }

            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            if (!user.EmailConfirmed)
            {
                context.SetError("Email Confirm", "Your Account Not Yet Varification, Please Check Your Email");
                return;
            }

            if (user.LockoutEnabled)
            {
                context.SetError("Lock", "Sory Your Account Is Lock, Please Contact Trireksa Administrator");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager);
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager);
            AuthenticationProperties properties = CreateProperties(user.UserName,user.Roles);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName, List<string> roles)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };

            if (roles != null && roles.Count > 0)
                data.Add("roles", roles[0]);
            return new AuthenticationProperties(data);
        }
    }
}