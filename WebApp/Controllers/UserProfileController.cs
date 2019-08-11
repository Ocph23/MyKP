using DataAccesLayer;
using DataAccesLayer.Models;
using DataAccesLayer.UnitOfWork;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MySql.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Security;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UserProfileController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            UOWUserProfile context = new UOWUserProfile();
            try
            {
                var result = context.GetPetugas();
                return Ok(result);
            }
            catch (Exception ex) 
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int id, petugas item)
        {
            UOWUserProfile context = new UOWUserProfile();
            try
            {
                var result = context.UpdatePetugas(item);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }






        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("api/UserProfile/Lock")]
        public IHttpActionResult LockAccount(string email)
        {
            try
            {
                var user = UserManager.FindByEmail(email);
                        if(user!=null)
                {
                    UserManager.SetLockoutEnabled(user.Id, true);
                    return Ok("Success");
                }

                throw new SystemException("User Status Not Change");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("api/UserProfile/UnLock")]
        public IHttpActionResult UnLockAccount(string email)
        {
            try
            {
                var user = UserManager.FindByEmail(email);
                if (user != null)
                {
                    UserManager.SetLockoutEnabled(user.Id, false);
                    return Ok("Success");
                }

                throw new SystemException("User Status Not Change");
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpGet]
        [Authorize(Roles = "Admin,Accounting,Courier")]
        [Route("api/UserProfile/GetAdminUserProfile")]
        public IHttpActionResult GetAdminUserProfile()
        {
            try
            {
                UOWUserProfile context = new UOWUserProfile();
                var userid = User.Identity.GetUserId();
                if (string.IsNullOrEmpty(userid))
                    throw new SystemException("Anda Tidak Memiliki Akses");
                return Ok(context.GetAdminProfile(userid));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Agent")]
        [Route("api/UserProfile/agent")]
        public IHttpActionResult GetUserAgentProfile()
        {
            try
            {
                UOWUserProfile context = new UOWUserProfile();
                var userid = User.Identity.GetUserId();
                if (string.IsNullOrEmpty(userid))
                    throw new SystemException("Anda Tidak Memiliki Akses");
                return Ok(context.GetUserProfile(userid));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        private bool ValidateData(petugas data)
        {

            if (string.IsNullOrEmpty(data.Role))
                return false;

            return true;
        }


        [HttpPost]
        [Route("api/UserProfile/Admin")]
        public async Task<IHttpActionResult> AddNewUser(petugas item)
        {
            UOWUserProfile context = new UOWUserProfile();
            IdentityResult result = null;
            var userModel = new Models.ApplicationUser { Email = item.Email, UserName = item.Email };
            try
            {

                if (item==null || !ValidateData(item))
                    throw new SystemException("Lengkapi Data Anda");


                Random rand = new Random();
                var password = Helper.GetRandomAlphanumericString(6) + "3#";
                result = await UserManager.CreateAsync(userModel, password);
                if (result.Succeeded)
                {
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(userModel.Id);
                    System.Web.Mvc.UrlHelper urlHelper = new System.Web.Mvc.UrlHelper(HttpContext.Current.Request.RequestContext, RouteTable.Routes);
                    string callbackUrl = urlHelper.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = userModel.Id, code = code },
                        HttpContext.Current.Request.Url.Scheme
                        );

                    await UserManager.SendEmailAsync(userModel.Id, "Confirm your account", "Your Password : " + password + " , and Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");

                    if (!await RoleManager.RoleExistsAsync(item.Role))
                    {
                        var roleCreate = RoleManager.Create(new IdentityRole(Guid.NewGuid().ToString(), item.Role));
                        if (!roleCreate.Succeeded)
                            throw new SystemException("User Tidak Berhasil Ditambah");
                    }
                    var addUserRole = await UserManager.AddToRoleAsync(userModel.Id, item.Role);

                    if (!addUserRole.Succeeded)
                    {
                        throw new SystemException("User Tidak Berhasil Ditambah");
                    }

                    item.UserId = userModel.Id;
                    var user = context.AddNewUser(item);
                    if (user != null)
                    {
                        return Ok(user);
                    }
                }
                throw new SystemException("User Tidak Berhasil Ditambah");

            }
            catch (Exception ex)
            {
                if (result != null && result.Succeeded)
                    UserManager.Delete(userModel);
                return BadRequest(ex.Message);
            }
        }


    }
}
