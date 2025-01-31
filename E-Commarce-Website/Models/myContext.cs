using Microsoft.EntityFrameworkCore;
namespace E_Commarce_Website.Models
{
    public class myContext:DbContext
    {
        public myContext(DbContextOptions<myContext>options):base(options) 
        {
            
        }
        public DbSet<Admin> tbl_admin {  get; set; } 
    }
}
