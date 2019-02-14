using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    [TableName("invoiceitem")]
    public class invoiceitem 
    {
        [PrimaryKey("Id")]
        [DbColumn("Id")]
        public int Id { get; set; }

        [DbColumn("InvoiceId")]
        public int InvoiceId { get; set; }

        [DbColumn("STTId")]
        public int STTId { get; set; }

        [DbColumn("Price")]
        public double Price { get; set; }

        [DbColumn("OtherCosts")]
        public double OtherCosts { get; set; }

        public stt STT { get; set; }

        public double Costs => GetCosts();

        private double GetCosts()
        {
              if(STT!=null)
                   return (STT.WeightValue * Price) + OtherCosts;
            return 0;
        }


    }

}
