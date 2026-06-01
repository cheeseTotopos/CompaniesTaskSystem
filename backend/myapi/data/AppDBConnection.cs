using Microsoft.EntityFrameworkCore;

public class AppDBConection : DbContext
{
    public DbSet<Company> Companies {get; set;}
    public DbSet<User> Users {get; set;}
    public DbSet<TaskStatus> TaskStatus {get; set;}
    public DbSet<Task> Tasks {get; set;}

    //the base is to call the father constructor (DhContext Constructor) 
    public AppDBConection(DbContextOptions<AppDBConection> options):base(options)
    {
    }
}