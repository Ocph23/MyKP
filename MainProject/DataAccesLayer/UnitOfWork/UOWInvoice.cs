using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesLayer.Models;
using Ocph.DAL;

namespace DataAccesLayer.UnitOfWork
{
    public class UOWInvoice
    {
        public object Get()
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var invoices =(from a in  db.Invoices.Select()
                                   join b in db.Agents.Select() on a.AgentId equals b.AgentId
                                   select new Invoice { AgentId=a.AgentId, CreatedDate=a.CreatedDate, DeadLine=a.DeadLine,
                                    Id=a.Id, Number=a.Number,  PetugasId=a.PetugasId, SendInvoiceCost=a.SendInvoiceCost,
                                    Tax=a.Tax, Agent=b}).ToList();

                    foreach(var item in invoices)
                    {
                        item.Items =(from a in  db.InvoiceItems.Where(O => O.InvoiceId == item.Id)
                                     join b in db.STT.Select() on a.STTId equals b.Id
                                     select new invoiceitem { Id=a.Id, InvoiceId=a.InvoiceId, OtherCosts=a.OtherCosts, Price=a.Price, STTId=a.STTId, STT=b }).ToList();

                        item.Payments = db.Payments.Where(O => O.InvoiceId == item.Id).ToList();
                    }
                    return invoices.OrderByDescending(O=>O.Number);
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }


        public object Get(int AgentId)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var invoices = (from a in db.Invoices.Where(O=>O.AgentId==AgentId)
                                    join b in db.Agents.Select() on a.AgentId equals b.AgentId
                                    select new Invoice
                                    {
                                        AgentId = a.AgentId,
                                        CreatedDate = a.CreatedDate,
                                        DeadLine = a.DeadLine,
                                        Id = a.Id,
                                        Number = a.Number,
                                        PetugasId = a.PetugasId,
                                        SendInvoiceCost = a.SendInvoiceCost,
                                        Tax = a.Tax,
                                        Agent = b
                                    }).ToList();

                    foreach (var item in invoices)
                    {
                        item.Items = (from a in db.InvoiceItems.Where(O => O.InvoiceId == item.Id)
                                      join b in db.STT.Select() on a.STTId equals b.Id
                                      select new invoiceitem { Id = a.Id, InvoiceId = a.InvoiceId, OtherCosts = a.OtherCosts, Price = a.Price, STTId = a.STTId, STT = b }).ToList();

                        item.Payments = db.Payments.Where(O => O.InvoiceId == item.Id).ToList();
                    }
                    return invoices.OrderByDescending(O => O.Number);
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }

        public object GetInvoiceByNumber(int id)
        {
            throw new NotImplementedException();
        }

        public Invoice Create(Invoice value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var newNumber = GetNewNumber();
                    value.Number = newNumber;
                    value.CreatedDate = DateTime.Now;
                    value.DeadLine = DateTime.Now.AddDays(14);
                    value.Id = db.Invoices.InsertAndGetLastID(value);
                    if (value.Id > 0)
                        return value;
                    throw new SystemException();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw new SystemException("Invoice Gagal Dibuat");
                }
            }
         

            
        }

        public object Getitems(int id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var command = db.CreateCommand();
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "SttNotPaid";
                    command.Parameters.Add(new MySql.Data.MySqlClient.MySqlParameter("id", id));
                     var result= command.ExecuteReader();
                  var datas=  Ocph.DAL.Mapping.MySql.MappingProperties<stt>.MappingTable(result);

                    var list = new List<invoiceitem>();
                    if (datas != null)
                    {
                        foreach(var data in datas)
                        {
                            if(data.Id>0)
                            {
                                var item = new invoiceitem { STT = data, STTId = data.Id , Price=data.PriceValue};
                                list.Add(item);
                            }
                         
                        }
                        return list;
                    }

                    throw new SystemException("Data Tidak Ada");
                    
                }
                catch (Exception ex)
                {

                    throw new SystemException(ex.Message);
                }
            }
        }

        private int GetNewNumber()
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var result = db.Invoices.GetLastItem();
                    if (result != null)
                        return result.Number + 1;
                    else
                        return 1;
                }
                catch (Exception)
                {

                    throw new SystemException();
                }
            }
        }

        public Invoice Update(Invoice value)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                   var updated= db.Invoices.Update(O=>new { O.DeadLine,O.SendInvoiceCost,O.Status,O.Tax},value,O=>O.Id==value.Id);
                    if (updated)
                        return value;
                    throw new SystemException();
                }
                catch (Exception)
                {

                    throw new SystemException("Invoice Gagal Dibuat");
                }
            }
        }

       
        

        public invoiceitem UpdateItem(int id, invoiceitem data)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var invoice = db.Invoices.Where(O => O.Id == id).FirstOrDefault();
                    if (invoice != null)
                    {
                        var updated= db.InvoiceItems.Update(O => new { O.OtherCosts, O.Price }, data, O => O.Id == data.Id);
                        if (updated)
                            return data;
                    }

                    throw new SystemException();
                }
                catch (Exception)
                {

                    throw new SystemException("Invoice Item Gagal Ditambah");
                }
            }
        }

        public object AddNewItem(int id, IEnumerable<invoiceitem> datas)
        {
            using (var db = new OcphDbContext())
            {
                var trans = db.BeginTransaction();
                try
                {
                    var invoice = db.Invoices.Where(O => O.Id==id).FirstOrDefault();
                    if (invoice != null)
                    {
                        foreach(var data in datas)
                        {
                            data.Id = db.InvoiceItems.InsertAndGetLastID(data);
                            if (data.Id <=0)
                                throw new SystemException() ;
                        }
                        trans.Commit();
                        return datas;
                    }
                    throw new SystemException();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw new SystemException("Invoice Item Gagal Ditambah");
                }
            }
        }

        public bool DeleteItem(int id)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                    var deleted = db.InvoiceItems.Delete(O => O.Id == id);
                    if (deleted)
                    {
                        return true;
                    }

                    throw new SystemException();
                }
                catch (Exception)
                {

                    throw new SystemException("Invoice Item Gagal Ditambah");
                }
            }
        }

        //Pembayaran
        public pembayaran Payment(pembayaran bayar)
        {
            using (var db = new OcphDbContext())
            {
                try
                {
                     if(bayar.Id<=0)
                    {
                        bayar.Id = db.Payments.InsertAndGetLastID(bayar);
                        if (bayar.Id > 0)
                            return bayar;
                    }
                    else
                    {
                        var saved = db.Payments.Update(O => new { O.DateOfPayment, O.Note, O.Status,O.verification }, bayar, O => O.Id == bayar.Id);
                        if (saved)
                            return bayar;
                    }

                    throw new SystemException();
                }
                catch (Exception)
                {
                    throw new SystemException("Data Tidak Tersimpan");
                }
            }
        }

    }
}
