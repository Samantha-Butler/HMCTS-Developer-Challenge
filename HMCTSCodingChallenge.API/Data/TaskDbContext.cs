using Microsoft.EntityFrameworkCore;
using HMCTSCodingChallenge.API.Models;

namespace HMCTSCodingChallenge.API.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options) { }

        public DbSet<TaskModel> Tasks { get; set; }
    }
}
