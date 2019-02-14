using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ocph.DAL;
 
 namespace DataAccesLayer.Models 
{ 
     [TableName("stt")] 
     public class stt 
   {
          [PrimaryKey("Id")] 
          [DbColumn("Id")] 
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value;
                     }
          } 

          [DbColumn("STT")] 
          public string STT 
          { 
               get{return _stt;} 
               set{ 
                      _stt=value;
                     }
          } 

          [DbColumn("Shiper")] 
          public string Shiper 
          { 
               get{return _shiper;} 
               set{ 
                      _shiper=value;
                     }
          } 

          [DbColumn("Reciever")] 
          public string Reciever 
          { 
               get{return _reciever;} 
               set{ 
                      _reciever=value;
                     }
          }

        [DbColumn("ShiperAddress")]
        public string ShiperAddress
        {
            get { return _shiperAddress; }
            set
            {
                _shiperAddress = value;
            }
        }

        [DbColumn("RecieverAddress")]
        public string RecieverAddress
        {
            get { return _recieverAddress; }
            set
            {
                _recieverAddress = value;
            }
        }



        [JsonConverter(typeof(StringEnumConverter))]
        [DbColumn("WeightType")] 
          public WeightType WeightType 
          { 
               get{return _weighttype;} 
               set{ 
                      _weighttype=value;
                     }
          } 

          [DbColumn("Pcs")] 
          public int Pcs 
          { 
               get{return _pcs;} 
               set{ 
                      _pcs=value;
                     }
          } 

          [DbColumn("WeightValue")] 
          public double WeightValue 
          { 
               get{return _weightvalue;} 
               set{ 
                      _weightvalue=value;
                     }
          } 

          [DbColumn("ManifestId")] 
          public int ManifestId 
          { 
               get{return _manifestid;} 
               set{ 
                      _manifestid=value;
                     }
          }


        [JsonConverter(typeof(StringEnumConverter))]
        [DbColumn("ShippingBy")]
        public PortType ShippingBy { get; set; }

        [DbColumn("CityId")]
        public int CityId { get; set; }



        public double PriceValue { get; set; }
        public status Status { get; set; }
        public manifest Manifest { get; set; }
        public city City { get; set; }

        private int  _id;
           private string  _stt;
           private string  _shiper;
           private string  _reciever;
           private WeightType _weighttype;
           private int  _pcs;
           private double  _weightvalue;
           private int  _manifestid;
        private string _shiperAddress;
        private string _recieverAddress;
    }
}


