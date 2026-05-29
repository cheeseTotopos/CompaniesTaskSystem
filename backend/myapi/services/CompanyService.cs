using Microsoft.EntityFrameworkCore;

public class CompanyService(AppDBConection _conn)
{
    public async Task<ResponseFormat<object>> AddCompany(CompanyDTO data)
    {
        var companyNameTrimmed = data.CompanyName.Trim();

        //check if company name exists
        var nameExists = await CompanyExist(companyNameTrimmed);
        if(nameExists == true)
            return new ResponseFormat<object>
            {
                Success = false,
                Message = "El nombre de compañía '" + companyNameTrimmed + "' ya existe",
                Data = null
            };

        var company = new Company
        {
            CompanyName = companyNameTrimmed,
            Pwd = BCrypt.Net.BCrypt.HashPassword(data.Pwd),
            IsActive = 1
        };

        _conn.Companies.Add(company);

        await _conn.SaveChangesAsync();

        //after saving the changes, entitiy framework updates the company object, setting the id
        return new ResponseFormat<object>
            {
                Success = true,
                Message = "Compañía" + companyNameTrimmed + " creada con éxito",
                Data = new {companyId = company.Id}
            };
    }

    public async Task<bool> CompanyExist(string name)
    {
        var exists = await _conn.Companies.AnyAsync(c => c.CompanyName == name);
        
        return exists;
    }

    public async Task<bool> CompanyExist(int id)
    {
        var exists = await _conn.Companies.AnyAsync(c => c.Id == id); 
        
        return exists;
    }
}