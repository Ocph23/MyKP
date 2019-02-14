using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
 namespace TrireksaTracingApps.Models 
{ 
     public class petugas  
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

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
            }
        }


          public string Address 
          { 
               get{return _address;} 
               set{ 
                      _address=value;
                     }
          } 

          public string Handphone 
          { 
               get{return _handphone;} 
               set{ 
                      _handphone=value;
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

        private string role;

        public string Role
        {
            get { return role; }
            set { role = value; }
        }




        private int  _id;
           private string  _name;
           private string  _address;
           private string  _handphone;
        private string _userid;
        private string _email;
    }
}


