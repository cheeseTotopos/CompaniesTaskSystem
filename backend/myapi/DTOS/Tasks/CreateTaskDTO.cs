using System.ComponentModel.DataAnnotations;

public class CreateTaskDTO()
{
    [Required]
    public string Title {get; set;} = string.Empty;
    [Required]
    public string TaskDescription {get; set;} = string.Empty;
    [Required]
    public int CompanyId {get; set;}
    [Required]
    public int StatusId {get; set;}
    [Required]
    public int IsPriority {get; set;}
    [Required]
    public DateOnly DueDate {get; set;}
    [Required]
    public int CreatedBy {get; set;}
    [Required]
    public int UserId {get; set;}
}