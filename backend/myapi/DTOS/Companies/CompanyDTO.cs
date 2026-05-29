using System.ComponentModel.DataAnnotations;

public class CompanyDTO()
{
    [Required]
    public string CompanyName {get; set;} = string.Empty;
    [Required]
    public string Pwd {get; set;} = string.Empty;
}