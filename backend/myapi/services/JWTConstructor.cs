
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

public class JWTConstructor(IConfiguration conf)
{
    public string TokenGenerator(User data)
    {
        //in here we access to our jwt secret using the conf object
        string? secretKey = conf["jwt"];
        if (secretKey == null)
            throw new Exception("JWT Key not configured");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var signed = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, data.Id.ToString()),//sub is for an id
            new Claim(JwtRegisteredClaimNames.Email, data.Email)
        };

        var rawToken = new JwtSecurityToken(
            issuer: "http://myapi",
            audience: "http://myfrontend", 
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: signed
        );

        var token = new JwtSecurityTokenHandler().WriteToken(rawToken);

        return token;
        
    }
}