using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
 namespace TrireksaTracingApps.Models 
{ 
     public class agentadmin 
   {
         
          public int Id 
          { 
               get{return _id;} 
               set{ 
                      _id=value;
                     }
          } 

          public string Name 
          { 
               get{return _name;} 
               set{ 
                      _name=value;
                     }
          } 

          public int AgentId 
          { 
               get{return _agentid;} 
               set{ 
                      _agentid=value;
                     }
          }

        public string UserId
        {
            get { return _userid; }
            set
            {
                _userid = value;
            }
        }

        public string Email { get; set; }

        public bool Status { get; set; }

        public string Address { get;  set; }

        public string Telepon { get;  set; }

        public byte[] Photo { get;  set; }

        private int  _id;
           private string  _name;
           private int  _agentid;
        private string _userid;
    }
}


