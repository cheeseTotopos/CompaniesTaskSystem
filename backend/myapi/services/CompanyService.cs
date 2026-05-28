public class CompanyService(AppDBConection _conn)
{
    public async Task<int> AddCompany(CompanyDTO data)
    {
        var company = new Company
        {
            CompanyName = data.CompanyName,
            Pwd = data.Pwd,
            IsActive = 1
        };

        _conn.Companies.Add(company);

        await _conn.SaveChangesAsync();

        //after saving the changes, entitiy framework updates the company object, setting the id
        return company.Id;
    }
}