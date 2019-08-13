using DataAccesLayer.Models;
using DataAccesLayer.UnitOfWork;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApp.Controllers
{
    public class PenjualanController : ApiController
    {
        // GET: api/Penjualan
        UOWPenjualan uow = new UOWPenjualan();
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/penjualan/byadmin")]
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IHttpActionResult GetByAdmin(DateTime dari, DateTime sampai)
        {
            try
            {
                return Ok(uow.GetByAdmin(dari, sampai));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // POST: api/Penjualan
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Penjualan/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Penjualan/5
        public void Delete(int id)
        {
        }



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

    }
}
