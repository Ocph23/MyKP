using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccesLayer.UnitOfWork;
using Microsoft.AspNet.Identity;

namespace WebApp.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AgentController : ApiController
    {

        UOWAgent agentContext = new UOWAgent();
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


        // POST: api/Agent
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Agent/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Agent/5
        public void Delete(int id)
        {
        }




        [Route("agent/getuserprofile")]
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
    }
}
