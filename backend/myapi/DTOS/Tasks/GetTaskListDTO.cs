using System.ComponentModel.DataAnnotations;

public class GetTaskListDTO()
{
    [Required]
    public int CompanyId {get; set;}
}