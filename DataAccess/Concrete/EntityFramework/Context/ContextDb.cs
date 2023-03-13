using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class ContextDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*integrated security
             * inegrated security'i true yapiyoruz cunku veri tabanini bir sunucu uzerinden ya da bir kullanici girisi yapilarak calistirmiyoruz. eger bahsi gecen her iki turden bir erisim olacaksa veri tabanina burada integrated security'i false olarak belirtmemiz gerekmektedir.
             */
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-1C9EMR6;Initial Catalog=eReconciliationDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        }       

        public DbSet<AccountReconciliation> AccountReconciliations { get; set; }
        public DbSet<AccountReconciliationDetail> AccountReconciliationDetails { get; set; }
        public DbSet<BaBsReconciliation> BaBsReconciliations { get; set; }
        public DbSet<BaBsReconciliationDetail> BaBsReconciliationDetails { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyAccount> CurrencyAccounts { get; set; }
        public DbSet<MailParameter> MailParameters { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
