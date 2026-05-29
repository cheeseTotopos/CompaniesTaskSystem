using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
public class AuthController(UserService _us, JWTConstructor _jwtconstructor) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDTO data)
    {
        var response = await _us.Register(data);

        if(response.Success == false)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDTO data)
    {
        var result = await _us.Login(data);
        if(result.Success == false || result.Data == null)
            return BadRequest(result.Message);

        var token = _jwtconstructor.TokenGenerator(result.Data);
        return Ok(new
        {
            Success = result.Success,
            Message = result.Message,
            Data = result.Data,
            Token = token
        });
    }
}