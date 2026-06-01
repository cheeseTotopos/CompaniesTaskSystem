using System.ComponentModel.DataAnnotations;

public class CreateTaskStatusDTO()
{
    [Required]
    public string StatusName {get; set;} = string.Empty;
    [Required]
    public int CompanyId {get; set;}
    [Required]
    public int CreatedBy {get; set;}

}