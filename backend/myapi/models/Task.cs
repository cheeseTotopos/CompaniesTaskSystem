public class Task()
{
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string TaskDescription {get; set;} = string.Empty;
    public int StatusId {get; set;}
    public int IsPriority {get; set;}
    public DateOnly DueDate {get; set;}
    public int CreatedBy {get; set;}
    public int UserId {get; set;}
}