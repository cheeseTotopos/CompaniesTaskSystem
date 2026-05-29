using Microsoft.AspNetCore.Mvc;


[ApiController]

[Route("company")]
public class CompanyController(CompanyService _cs) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CompanyDTO data)
    {
        var result = await _cs.AddCompany(data);

        if(result.Success == false)
            return BadRequest(result);

        return Ok(result);
    }
}