using DataAccesLayer.Models;
using DataAccesLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Controllers
{
    public class InvoiceController : ApiController
    {
        // GET: api/Invoice
        UOWInvoice context = new UOWInvoice();
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
       

        // POST: api/Agent
        public IHttpActionResult Post([FromBody]Invoice value)
        {
            try
            {

                if (value == null)
                    throw new SystemException("Periksa Kembali Data Anda");

                return Ok(context.Create(value));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put(int id, [FromBody]Invoice value)
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



        [Route("api/invoice/getitems")]
        public IHttpActionResult GetSTTNotPaidByAgentId(int id)
        {
            //id is invoice id
            try
            {
                return Ok(context.Getitems(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Route("api/invoice/additem")]
        public IHttpActionResult AddItem(int id, IEnumerable<invoiceitem> data)
        {
            //id is invoice id
            try
            {
                if (data == null)
                    throw new SystemException("Periksa Kembali Data Anda");
                return Ok(context.AddNewItem(id, data));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("api/invoice/updateitem")]
        public IHttpActionResult UpdateItem(int id, invoiceitem data)
        {
            //id is invoice id
            try
            {
                if (data == null)
                    throw new SystemException("Periksa Kembali Data Anda");
                return Ok(context.UpdateItem(id, data));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("api/invoice/deleteitem")]
        public IHttpActionResult DeleteItem(int id)
        {
            //id is item id

            try
            {
                return Ok(context.DeleteItem(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        //pembayaran
        [Route("api/invoice/getbyagentid")]
        public IHttpActionResult GetInvoice(int id)
        {
            //id is invoice id
            try
            {
                return Ok(context.Get(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [Route("api/invoice/addpembayaran")]
        public IHttpActionResult AddPembayaran(pembayaran data)
        {
            try
            {
                if (data == null)
                    throw new SystemException("Periksa Kembali Data Anda");
                return Ok(context.Payment(data));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Route("api/invoice/VerificationPayment")]
        public IHttpActionResult VerificationPayment(pembayaran data)
        {
            try
            {
                if (data == null)
                    throw new SystemException("Periksa Kembali Data Anda");
                return Ok(context.Payment(data));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
