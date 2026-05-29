using Microsoft.EntityFrameworkCore;

public class UserService(AppDBConection _conn, CompanyService _cs)
{
    public async Task<ResponseFormat<object>> Register(UserDTO data)
    {
        //cehck if the company exists
        var  companyExists = await _cs.CompanyExist(data.CompanyId);
        if(companyExists == false)
            return new ResponseFormat<object>
            {
                Success = false,
                Message = "Esta compañía no existe",
                Data = null
            };

        //check if the mail is duplicated
        var duplicatedEmail = await DuplicatedEmail(data.Email.Trim());
        if(duplicatedEmail == true)
            return new ResponseFormat<object>
            {
                Success = false,
                Message = "Este email ya se encuentra registrado",
                Data = null
            };


        var validName  = data.FullName.Trim();
        validName = validName.ToUpper();
        var user = new User
        {
            FullName = validName,
            Email = data.Email.Trim(),
            CreatedAt = DateOnly.FromDateTime(DateTime.Now),
            CompanyId = data.CompanyId
        };

        _conn.Users.Add(user);
        await _conn.SaveChangesAsync();

        return new ResponseFormat<object>
            {
                Success = true,
                Message = "Usuario registrado con éxito",
                Data = user
            };;
    }

    public async Task<ResponseFormat<User?>> Login(UserDTO data)
    {
        //check if the user exists
        var user = await UserExists(data.FullName, data.Email, data.CompanyId);
        if (user.Success == false)
            return new ResponseFormat<User?>
            {
                Success = false,
                Message = "No existe un usuario con estos datos",
                Data = null
            };
        
        return new ResponseFormat<User?>
        {
            Success = true,
            Message = "Usuario encontrado",
            Data = user.Data
        };
    }

    public async Task<bool> DuplicatedEmail(string email)
    {
        var duplicated = await _conn.Users.AnyAsync(e => e.Email == email);

        return duplicated;
    }

    public async Task<ResponseFormat<User?>> UserExists(string fullName, string email, int companyId)
    {
        var user = await _conn.Users.FirstOrDefaultAsync(u => u.FullName == fullName && u.Email == email && u.CompanyId == companyId);

        var success = false;
        if(user != null)
            success = true;
        return new ResponseFormat<User?>
        {
            Success = success,
            Message = "",
            Data = user
        };
    }
}