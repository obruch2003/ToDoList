using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=ToDoList;Integrated Security=True;TrustServerCertificate=True;");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Files> Files { get; set; }

        public DbSet<Comments> Comments { get; set; }
    }
}
