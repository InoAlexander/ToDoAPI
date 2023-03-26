using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;

namespace ToDoAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }

        // db set is a representation of the TODO items in our database
        public DbSet<ToDo> ToDos => Set<ToDo>();
    }
}
