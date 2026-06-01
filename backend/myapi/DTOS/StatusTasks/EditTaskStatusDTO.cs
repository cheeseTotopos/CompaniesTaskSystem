using System.ComponentModel.DataAnnotations;

public class EditTaskStatusDTO()
{

    [Required]
    public int Id {get; set;}
    [Required]
    public string NewName {get; set;} = string.Empty;
    [Required]
    public int CompanyId {get; set;}
    [Required]
    //we need the id of the user that created the status because hes the only allowed to edit his status
    public int CreatedBy {get; set;}
}