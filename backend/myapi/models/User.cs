using System.ComponentModel.DataAnnotations.Schema;

[Table("Users")]
public class User()
{
    public int Id {get; set;}
    public string FullName {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public DateOnly CreatedAt {get; set;}

    [Column("Companie")]
    public int CompanyId {get; set;}
    public ICollection<TaskStatus> TaskStatus = new List<TaskStatus>();
    public ICollection<Task> CreatedTask = new List<Task>();
    public ICollection<Task> AssignedTask = new List<Task>();
}