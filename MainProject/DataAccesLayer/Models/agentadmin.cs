using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace DataAccesLayer.Models 
{ 
     [TableName("agentadmin")] 
     public class agentadmin 
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

          [DbColumn("Name")] 
          public string Name 
          { 
               get{return _name;} 
               set{ 
                      _name=value;
                     }
          } 

          [DbColumn("AgentId")] 
          public int AgentId 
          { 
               get{return _agentid;} 
               set{ 
                      _agentid=value;
                     }
          }

        [DbColumn("UserId")]
        public string UserId
        {
            get { return _userid; }
            set
            {
                _userid = value;
            }
        }

        [DbColumn("Email")]
        public string Email { get; set; }

        [DbColumn("Status")]
        public bool Status { get; set; }

        [DbColumn("Address")]
        public string Address { get;  set; }

        [DbColumn("Telepon")]
        public string Telepon { get;  set; }

        [DbColumn("Photo")]
        public byte[] Photo { get;  set; }

        private int  _id;
           private string  _name;
           private int  _agentid;
        private string _userid;
    }
}


