using System.ComponentModel.DataAnnotations;

public class UserDTO()
{
    public int Id {get; set;}
    [Required]
    public string FullName {get; set;} = string.Empty;
    [Required]
    [EmailAddress]
    public string Email {get; set;} = string.Empty;
    [Required]
    public int CompanyId {get; set;}
}