using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Table("Tasks")]
public class Task()
{
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string TaskDescription {get; set;} = string.Empty;
    public int CompanyId {get; set;}
    public Company Company {get; set;}= null!;
    public int StatusId {get; set;}
    [ForeignKey(nameof(StatusId))]
    public TaskStatus TaskStatus {get; set;} = null!;
    public int IsPriority {get; set;}
    public DateOnly DueDate {get; set;}
    public int CreatedBy {get; set;}
    [ForeignKey(nameof(CreatedBy))]
    public User Creator {get; set;} = null!;
    //the userId its the user that owns or has to realieze or complete the task. It could be different than the person that creates the task
    public int UserId {get; set;}
    [ForeignKey(nameof(UserId))]
    public User AssignedUser { get; set; } = null!;
}