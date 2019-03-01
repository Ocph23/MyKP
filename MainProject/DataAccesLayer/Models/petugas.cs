using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ocph.DAL;
 
 namespace DataAccesLayer.Models 
{ 
     [TableName("petugas")] 
     public class petugas  
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

        [DbColumn("Email")]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
            }
        }


        [DbColumn("Address")] 
          public string Address 
          { 
               get{return _address;} 
               set{ 
                      _address=value;
                     }
          } 

          [DbColumn("Handphone")] 
          public string Handphone 
          { 
               get{return _handphone;} 
               set{ 
                      _handphone=value;
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

        private string role;

        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        public List<role> Roles { get; set; }

        private int  _id;
           private string  _name;
           private string  _address;
           private string  _handphone;
        private string _userid;
        private string _email;
    }
}


