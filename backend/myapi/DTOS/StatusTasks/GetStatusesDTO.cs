using System.ComponentModel.DataAnnotations;

public class GetStatusesDTO()
{
    [Required]
    public int CompanyId {get; set;}
}