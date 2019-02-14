using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaTracingApps.Models
{
    public class invoiceitem 
    {
    
        public int Id { get; set; }

        public int InvoiceId { get; set; }

        public int STTId { get; set; }

        public double Price { get; set; }

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
