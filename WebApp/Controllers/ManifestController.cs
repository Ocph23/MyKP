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
    public class ManifestController : ApiController
    {
        // GET: api/Manifest
        UOWManifest context = new UOWManifest();
        // GET: api/City
     

        public IHttpActionResult GetByAgentId(int agentId)
        {
            try
            {
                return Ok(context.GetManifest(agentId));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetManifestById(int id)
        {
            try
            {
                return Ok(context.GetManifestById(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        // POST: api/Agent
        public IHttpActionResult Post([FromBody]manifest value)
        {
            try
            {
                if (value == null)
                    throw new SystemException("Periksa Kembali Data Anda");

                return Ok(context.CreateManifest(value));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put(int id, [FromBody]manifest value)
        {
            try
            {
                return Ok(context.UpdateManifest(value));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                return Ok(context.DeleteManifest(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [Route("api/manifest/additem")]
        public IHttpActionResult AddItem(int id, stt data)
        {
            try
            {
                if (data == null)
                    throw new SystemException("Periksa Kembali Data Anda");
                return Ok(context.AddNewItem(id,data));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //updateitem
        [HttpPut]
        [Route("api/manifest/updateitem")]
        public IHttpActionResult GetSttInfo(int id, stt item)
        {
            try
            {
                return Ok(context.UpdateItem(id, item));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Route("api/manifest/deleteitem")]
        public IHttpActionResult DeleteItem(int id)
        {
            try
            {
                return Ok(context.DeleteItem(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/manifest/find")]
        public IHttpActionResult GetSttInfo(int id, string stt)
        {
            try
            {
                
                return Ok(context.Find(id, stt));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/manifest/FindBySTT")]
        public IHttpActionResult FindBySTT(string stt)
        {
            try
            {
                
                return Ok(context.FindBySTT(stt));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/manifest/findManifestByAgentId")]
        public IHttpActionResult findManifestByAgentId(int agentid, int manifestNumber)
        {
            try
            {
                return Ok(context.ManifestByAgentId(agentid, manifestNumber));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



        ///courier
        ///

        [HttpGet]
        [Route("api/manifest/newmanifest")]
        public IHttpActionResult newmanifest()
        {
            try
            {
                return Ok(context.GetNewManifest());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



    }
}
