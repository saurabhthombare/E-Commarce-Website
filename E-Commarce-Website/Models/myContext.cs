using Microsoft.EntityFrameworkCore;
namespace E_Commarce_Website.Models
{
    public class myContext:DbContext
    {
        public myContext(DbContextOptions<myContext>options):base(options) 
        {
            
        }
        public DbSet<Admin> tbl_admin {  get; set; }
        public DbSet<Registration> tbl_Register { get; set; }
        public DbSet<Category> tbl_category { get; set; }
        public DbSet<Product> tbl_product { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                
                .HasOne(p => p.Category)
                .WithMany(c => c.Product)
                .HasForeignKey(p => p.cat_id);
            modelBuilder.Entity<Registration>().ToTable("tbl_Register");
        }
    }
}
