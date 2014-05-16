using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServerCompact;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Demo2Project.Models
{
  public class ApplicationConfiguration : DbConfiguration
  {
    public ApplicationConfiguration()
    {
      SetProviderServices(SqlCeProviderServices.ProviderInvariantName, SqlCeProviderServices.Instance);
      //SetDefaultConnectionFactory(new SqlCeConnectionFactory(SqlCeProviderServices.ProviderInvariantName));
      SetExecutionStrategy(SqlCeProviderServices.ProviderInvariantName, () => new DefaultExecutionStrategy());
    }

  }

  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext()
      : base(@"DefaultConnection")
    {
    }

    //public System.Data.Entity.DbSet<Demo2Project.Models.Temp> Temps { get; set; }
  }

  // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
  public class ApplicationUser : IdentityUser
  {
  }

  //public class Temp
  //{
  //  [Key]
  //  public long ID { get; set; }

  //  public string Name { get; set; }
  //}
}