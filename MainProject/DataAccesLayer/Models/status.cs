using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace DataAccesLayer.Models 
{ 
     [TableName("status")] 
     public class status  
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

          [DbColumn("RecieverName")] 
          public string RecieverName 
          { 
               get{return _recievername;} 
               set{ 
                      _recievername=value;
                     }
          } 

          [DbColumn("RecieveDate")] 
          public DateTime RecieveDate 
          { 
               get{return _recievedate;} 
               set{ 
                      _recievedate=value;
                     }
          } 

          [DbColumn("RecieveCondition")] 
          public string RecieveCondition 
          { 
               get{return _recievecondition;} 
               set{ 
                      _recievecondition=value;
                     }
          } 


          [DbColumn("STTId")] 
          public int STTId 
          { 
               get{return _sttid;} 
               set{ 
                      _sttid=value;
                     }
          } 

          [DbColumn("CourierId")] 
          public int CourierId 
          { 
               get{return _courierid;} 
               set{ 
                      _courierid=value;
                     }
          }

        [DbColumn("Sign")]
        public byte[] Sign { get { return _sign; } set { _sign = value; } }


        public petugas Courier { get; set; }


        private int  _id;
           private string  _recievername;
           private DateTime  _recievedate;
           private string  _recievecondition;
           private int  _sttid;
           private int  _courierid;
        private byte[] _sign;
    }
}


