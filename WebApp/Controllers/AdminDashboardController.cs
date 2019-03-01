using DataAccesLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Controllers
{
    public class AdminDashboardController : ApiController
    {
        UOWAdminDashboard context = new UOWAdminDashboard();
        [HttpGet]
        [Route("api/AdminDashboard/InvoiceCount")]
        public IHttpActionResult GetInvoiceCount()
        {
            try
            {
                return Ok(context.InvoiceCount());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/AdminDashboard/ManifestCount")]
        public IHttpActionResult GetManifestCount()
        {
            try
            {
                UOWManifest mancontext = new UOWManifest();
                return Ok(mancontext.GetNewManifest());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/AdminDashboard/STTNotHaveStatus")]
        public IHttpActionResult GetSTTNotHaveStatus()
        {
            try
            {
                return Ok(context.STTNotHaveStatus());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/AdminDashboard/InvoiceDeadLine")]
        public IHttpActionResult GetInvoiceDeadLine()
        {
            try
            {
                return Ok(context.InvoiceDeadLine());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
