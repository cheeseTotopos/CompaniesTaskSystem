using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Companies")]
public class Company
{
    //telling entity framework this is the table primary key
    [Key]
    public int Id {get; set;}

    //initializing for avoiding the empty string warning
    public string CompanyName {get; set;} = string.Empty;
    public string Pwd {get; set;} = string.Empty;
    public int IsActive {get; set;}

    //empty constructor needed for using entity framwork
    public Company(){}
}