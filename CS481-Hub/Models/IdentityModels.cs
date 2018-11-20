using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CS481_Hub.Models
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

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        //**Also important to note that migrations need to be done every time something within ApplicationDbContext gets changed.**
        //https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/migrations-and-deployment-with-the-entity-framework-in-an-asp-net-mvc-application
        //This link talks about migration changes



        public DbSet<USER_EXT> User_ext { get; set;}
        public DbSet<BLOGS> Blog { get; set;}
        public DbSet<Available_API> Available_APIs { get; set;}
        public DbSet<USER_API_XREF> USER_APIs { get; set;}


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }




        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}

