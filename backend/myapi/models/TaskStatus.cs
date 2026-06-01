using System.ComponentModel.DataAnnotations.Schema;

[Table("TaskStatus")]
public class TaskStatus()
{
    public int Id {get; set;}
    public string StatusName {get; set;} = string.Empty;
    public int IsActive {get; set;}

    [Column("Companie")]
    public int CompanyId {get; set;}
    public Company Company {get; set;} = null!;
    public int CreatedBy {get; set;}
    
    [ForeignKey(nameof(CreatedBy))]
    public User Creator {get; set;} = null!;
    public ICollection<Task> Task = new List<Task>();
}