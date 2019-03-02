using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    [TableName("aspnetroles")]
    public class role
    {
        [DbColumn("Id")]
        public string Id { get; set; }
        [DbColumn("Name")]
        public string Name { get; set; }
    }



    [TableName("aspnetuserroles")]
    public class userrole
    {
        [DbColumn("UserId")]
        public string UserId { get; set; }
        [DbColumn("RoleId")]
        public string RoleId { get; set; }
    }

   
}
