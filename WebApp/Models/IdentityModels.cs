using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccesLayer;
using Microsoft.AspNet.Identity;
using MySql.AspNet.Identity;

namespace WebApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }



    }

    public class ApplicationDbContext :MySQLDatabase
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public int GetUserCount()
        {
            try
            {
                using (var db = new OcphDbContext())
                {
                    var query = "SELECT COUNT(*) FROM  aspnetusers";
                    var command = db.CreateCommand();
                    command.CommandText = query;
                    command.CommandType = System.Data.CommandType.Text;
                    var result = command.ExecuteScalar();
                    var data = Convert.ToInt32(result);
                    return data;
                }
            }
            catch (Exception)
            {

                throw new SystemException("On Error");
            }
          
        }
    }
}