
using Microsoft.EntityFrameworkCore;

namespace Praticis.Framework.Worker.Data.Context
{
    public class Worker_Context : DbContext
    {
        public Worker_Context(DbContextOptions<Worker_Context> options)
            : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Worker_Context).Assembly);
        }
    }
}