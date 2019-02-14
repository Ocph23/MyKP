using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TrireksaTracingApps.Models 
{ 
     public class status  
   {
         
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value;
                     }
          } 

          public string RecieverName 
          { 
               get{return _recievername;} 
               set{ 
                      _recievername=value;
                     }
          } 

          public DateTime RecieveDate 
          { 
               get{return _recievedate;} 
               set{ 
                      _recievedate=value;
                     }
          } 

          public string RecieveCondition 
          { 
               get{return _recievecondition;} 
               set{ 
                      _recievecondition=value;
                     }
          } 


          public int STTId 
          { 
               get{return _sttid;} 
               set{ 
                      _sttid=value;
                     }
          } 

          public int CourierId 
          { 
               get{return _courierid;} 
               set{ 
                      _courierid=value;
                     }
          }

        public petugas Courier { get; set; }
        public byte[] Sign { get; internal set; }

        private int  _id;
           private string  _recievername;
           private DateTime  _recievedate;
           private string  _recievecondition;
           private int  _sttid;
           private int  _courierid;
      }
}


