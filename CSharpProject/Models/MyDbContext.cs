using Microsoft.EntityFrameworkCore;
using CSharpProject.Models;
public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }
    public DbSet<Student> Student { get; set; }
    public DbSet<Course> Course{ get; set; }
    public DbSet<Enrollment> ĐKIKH { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Sử dụng MySQL.EntityFrameworkCore để kết nối MySQL
        options.UseMySQL("server=localhost;database=csharpproject;user=root;password=68066666");
    }
}
