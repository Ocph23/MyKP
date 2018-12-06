using DataAccesLayer.Models;
using System;
using System.Web.Http;
using DataAccesLayer.UnitOfWork;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using MySql.AspNet.Identity;
using System.Web.Routing;
using System.Threading.Tasks;
using DataAccesLayer;

namespace WebApp.Controllers
{

    [Authorize]
    public class AgentController : ApiController
    {

        UOWAgent agentContext = new UOWAgent();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _managerRoleManager;

        // GET: api/Agent/5

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(agentContext.Get());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(agentContext.GetById(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        // POST: api/Agent
        public IHttpActionResult Post([FromBody]agent value)
        {
            try
            {
                if (value == null)
                    throw new SystemException("Periksa Kembali Data Anda");
                return Ok(agentContext.post(value));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Agent/5
        public IHttpActionResult Put(int id, [FromBody]agent value)
        {
            try
            {
                return Ok(agentContext.Update(value));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Agent/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                return Ok(agentContext.Delete(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }




        //Users

        [HttpGet]
        [Route("agent/AgentUsers")]
        public IHttpActionResult AgentUsers(int agentId)
        {
            try
            {
                return Ok(agentContext.AgentUsers(agentId));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("agent/AddNewUser")]
        public async Task<IHttpActionResult> AddNewUser(int agentId, agentadmin item)
        {
            IdentityResult result = null;
            var userModel = new Models.ApplicationUser { Email = item.Email, UserName = item.Email };
            try
            {
                Random rand = new Random();
                var password = Helper.GetRandomAlphanumericString(6)+"3#";
                result = await UserManager.CreateAsync(userModel,password);
                if(result.Succeeded)
                {
                     string code = await UserManager.GenerateEmailConfirmationTokenAsync(userModel.Id);
                    System.Web.Mvc.UrlHelper urlHelper = new System.Web.Mvc.UrlHelper(HttpContext.Current.Request.RequestContext, RouteTable.Routes);
                    string callbackUrl = urlHelper.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = userModel.Id, code = code },
                        HttpContext.Current.Request.Url.Scheme
                        );

                    await UserManager.SendEmailAsync(userModel.Id,"Confirm your account","Your Password : "+password+" , and Please confirm your account by clicking this link: <a href=\""+ callbackUrl + "\">link</a>");

                    if (!await RoleManager.RoleExistsAsync("agent"))
                    {
                        var roleCreate = RoleManager.Create(new IdentityRole("3","Agent"));
                        if (!roleCreate.Succeeded)
                            throw new SystemException("User Tidak Berhasil Ditambah");
                    }
                    var addUserRole= await UserManager.AddToRoleAsync(userModel.Id, "Agent");

                    if(!addUserRole.Succeeded)
                    {
                        throw new SystemException("User Tidak Berhasil Ditambah");
                    }

                    item.UserId = userModel.Id;
                    var user = agentContext.AddNewUser(agentId, item);
                    if (user != null)
                    {
                        return Ok(agentContext.AddNewUser(agentId, item));
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

        [HttpPut]
        [Route("agent/ChangeActive")]
        public IHttpActionResult ChangeActive(int agentId, agentadmin item)
        {
            try
            {
                return Ok(agentContext.ChangeActive(agentId, item));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("agent/UpdateProfile")]
        public IHttpActionResult UpdateProfile(int agentId, agentadmin item)
        {
            try
            {
                return Ok(agentContext.UpdateProfile(agentId, item));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles ="Agent")]
        [Route("api/Agent/getuserprofile")]
        public IHttpActionResult GetUserProfile()
        {
            try
            {
                UOWAgentProfile context = new UOWAgentProfile();
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

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _managerRoleManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _managerRoleManager = value;
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
    }
}
