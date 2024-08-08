using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BookCentreProject.Models;
namespace BookCentreProject.Data

{
    public class UserDbContext:IdentityDbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext>options)
            :base(options) 
        {
        }
        public DbSet<Book>Books { get; set; }
        
    }
}
