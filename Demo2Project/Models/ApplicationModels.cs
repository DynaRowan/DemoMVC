using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Data.Entity.SqlServerCompact;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Demo2Project.Models
{
  public class ApplicationConfiguration : DbConfiguration
  {
    public ApplicationConfiguration()
    {
      SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
      SetExecutionStrategy(SqlProviderServices.ProviderInvariantName, () => new DefaultExecutionStrategy());
    }

  }

  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext()
      : base(@"SqlServerConnection")
    {
    }

    public System.Data.Entity.DbSet<Demo2Project.Models.Premium> Premiums { get; set; }
  }

  // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
  public class ApplicationUser : IdentityUser
  {
  }

  public class Premium
  {
    [Key]
    public long ID { get; set; }
    public string PostalCode { get; set; }
    public string StreetNumber { get; set; }
    public string StreetNumberSuffix { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public decimal InsuranceWoonhuis { get; set; }
    public decimal InsuranceInboedel { get; set; }
    public decimal InsuranceBuiten { get; set; }
    public decimal InsuranceAvp { get; set; }
    public decimal InsuranceOngeval { get; set; }
    public decimal InsuranceReis { get; set; }
    public decimal InsuranceRechtsbijstand { get; set; }
    public bool Calculated { get; set; }
    public bool Inspection { get; set; }
    public string Ownership { get; set; }
  }
}