using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    public class STTStatus : stt
    {
        public int Number { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime SendedDate { get; set; }
    }
}
