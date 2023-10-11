using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Data
{
    public class TodoDbContext:IdentityDbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options):base(options)
        {
            
        }
        public DbSet<TodoItem> Todos { get; set; }
    }
}
