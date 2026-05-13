using Microsoft.EntityFrameworkCore;
using TODOAPI_DATABASE.Models;
namespace TODOAPI_DATABASE.DBContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
