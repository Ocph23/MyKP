using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
 namespace TrireksaTracingApps.Models 
{ 
     public class agent 
   {
       
          public int AgentId 
          { 
               get{return _agentid;} 
               set{ 
                      _agentid=value;
                     }
          } 

          public string Name 
          { 
               get{return _name;} 
               set{ 
                      _name=value;
                     }
          } 

          public string NPWP 
          { 
               get{return _npwp;} 
               set{ 
                      _npwp=value;
                     }
          } 

          public string Address 
          { 
               get{return _address;} 
               set{ 
                      _address=value;
                     }
          } 

          public string ContactPerson 
          { 
               get{return _contactperson;} 
               set{ 
                      _contactperson=value;
                     }
          } 

          public string Telepon 
          { 
               get{return _telepon;} 
               set{ 
                      _telepon=value;
                     }
          } 

          public string Handphone 
          { 
               get{return _handphone;} 
               set{ 
                      _handphone=value;
                     }
          } 

          public string Handphone2 
          { 
               get{return _handphone2;} 
               set{ 
                      _handphone2=value;
                     }
          } 

          public string Code 
          { 
               get{return _code;} 
               set{ 
                      _code=value;
                     }
          } 

          private int  _agentid;
           private string  _name;
           private string  _npwp;
           private string  _address;
           private string  _contactperson;
           private string  _telepon;
           private string  _handphone;
           private string  _handphone2;
           private string  _code;
      }
}


