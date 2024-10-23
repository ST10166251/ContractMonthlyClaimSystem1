namespace ContractMonthlyClaimSystem1
{
    using ContractMonthlyClaimSystem1.Models;
    using Microsoft.EntityFrameworkCore;
    

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<SupportingDocument> SupportingDocuments { get; set; }
    }

}
