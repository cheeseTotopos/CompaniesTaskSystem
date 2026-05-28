using Microsoft.AspNetCore.Mvc;


[ApiController]

[Route("company")]
public class CompanyController(CompanyService _cs) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CompanyDTO data)
    {
        //password and company name cannot be empty strings
        if(data.CompanyName == "" || data.Pwd == "")
            return BadRequest();

        var companyId = await _cs.AddCompany(data);
        return Ok(new
        {
            companyName = data.CompanyName,
            id = companyId,
            message = "Companía creada correctamente"
        });
    }
}