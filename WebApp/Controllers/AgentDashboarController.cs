using DataAccesLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Controllers
{
    public class AgentDashboarController : ApiController
    {
        UOWAgentDashboard context;
        [HttpGet]
        [Route("api/AgentDashboar/InvoiceCount/{id}")]
        public IHttpActionResult GetInvoiceCount(int id)
        {
            try
            {
                context = new UOWAgentDashboard(id);
                return Ok(context.InvoiceCount());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/AgentDashboar/ManifestCount/{id}")]
        public IHttpActionResult GetManifestCount(int id)
        {
            try
            {
                context = new UOWAgentDashboard(id);
                return Ok(context.ManifestCount());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/AgentDashboar/STTNotHaveStatus/{id}")]
        public IHttpActionResult GetSTTNotHaveStatus(int id)
        {
            try
            {
                context = new UOWAgentDashboard(id);
                return Ok(context.STTNotHaveStatus());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/AgentDashboar/InvoiceDeadLine/{id}")]
        public IHttpActionResult GetInvoiceDeadLine(int id)
        {
            try
            {
                context = new UOWAgentDashboard(id);
                return Ok(context.InvoiceDeadLine());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
