using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    public class penjualan
    {

        public DateTime Tanggal { get; set; }
        public string Agen { get; set; }

        public string STT { get; set; }

        public string Pengirim { get; set; }

        public string Penerima { get; set; }

        public double Pcs { get; set; }

        public double Berat { get; set; }

        public double Tarif { get; set; }


        public double Total { get {
                return Tarif * Berat;
            } }


    }
}
