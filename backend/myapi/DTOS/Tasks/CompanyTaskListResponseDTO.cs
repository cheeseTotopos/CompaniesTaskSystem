public class CompanyTaskListResponseDTO()
{
    //the task id
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string TaskDescription {get; set;} = string.Empty;
    public int CompanyId {get; set;}
    public string CompanyName {get; set;} = string.Empty;
    public int StatusId {get; set;}
    public string StatusName {get; set;} = string.Empty;
    public int IsPriority {get; set;}
    public DateOnly DueDate {get; set;}
    public int CreatedBy {get; set;}
    public string CreatedByName {get; set;} = string.Empty;
    public int UserId {get; set;}
    public string UserName {get; set;} = string.Empty;
}