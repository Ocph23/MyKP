using DataAccesLayer.Models;
using DataAccesLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace WebApp.Controllers
{
     [Authorize(Roles ="Admin,Courier")]
    public class StatusController : ApiController
    {

        UOWStatus context = new UOWStatus();
        // GET: api/City
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(context.Get());
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
                return Ok(context.GetById(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        // POST: api/Agent
        [Authorize(Roles = "Courier")]
        public IHttpActionResult Post([FromBody]status value)
        {
            try
            {
              var userId=  User.Identity.GetUserId();
                UOWUserProfile userCtx = new UOWUserProfile();
                var pet = userCtx.GetAdminProfile(userId);
                value.Courier = pet;
                value.CourierId = pet.Id;
                if (value == null)
                    throw new SystemException("Periksa Kembali Data Anda");
                if (value.Id > 0)
                    return Ok(context.Update(value));
                return Ok(context.post(value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Agent/5
        public IHttpActionResult Put(int id, [FromBody]status value)
        {
            try
            {
                return Ok(context.Update(value));
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
                return Ok(context.Delete(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}
