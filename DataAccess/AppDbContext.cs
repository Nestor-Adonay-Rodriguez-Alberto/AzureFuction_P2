using Microsoft.EntityFrameworkCore;


namespace DataAccess
{
    public class AppDbContext: DbContext
    {

        // CONSTRUCTOR:
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) 
        {

        } 
    }
}
