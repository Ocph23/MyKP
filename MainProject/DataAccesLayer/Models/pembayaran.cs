using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{

    [TableName("pembayaran")]
     public class pembayaran
    {

        [PrimaryKey("Id")]
        [DbColumn("Id")]
        public int Id { get; set; }

        [DbColumn("InvoiceId")]
        public int InvoiceId { get; set; }

        [DbColumn("amountpaid")]
        public double AmountPaid { get; set; }

        [DbColumn("dateofpayment")]
        public DateTime? DateOfPayment { get; set; }

        [DbColumn("Status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public InvoiceStatus Status { get; set; }

        [DbColumn("Note")]
        public string Note { get; set; }

           [DbColumn("verification")]
           public bool verification { get; set; }
    }
}
