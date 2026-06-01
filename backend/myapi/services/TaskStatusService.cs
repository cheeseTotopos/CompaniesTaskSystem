using Microsoft.EntityFrameworkCore;

public class TaskStatusService(AppDBConection _conn, CompanyService _cs, UserService _us)
{
    public async Task<ResponseFormat<TaskStatus?>> Create(CreateTaskStatusDTO data)
    {
        //check if the company exists
        var companyExists = await _cs.CompanyExist(data.CompanyId);
        if(companyExists == false)
            return new ResponseFormat<TaskStatus?>
            {
                Success = false,
                Message = "La compañía no existe",
                Data = null
            };

        //check if user exists
        var userExists = await _us.UserExists(data.CreatedBy);
        if(userExists == null)
            return new ResponseFormat<TaskStatus?>
            {
                Success = false,
                Message = "El usuario no existe",
                Data = null
            };

        //check if the user belongs to the company
        var userBelongs = await _us.UserBelongsToCompany(data.CreatedBy, data.CompanyId);
        if(userBelongs == false)
            return new ResponseFormat<TaskStatus?>
            {
                Success = false,
                Message = "El usuario no pertenece a la compañia",
                Data = null
            };
        
        var validName = data.StatusName.Trim();
        validName = validName.ToUpper();

        //check if theres no a company status with the same name
        var nameExists = await ExistStatus(validName, data.CompanyId);
        if (nameExists == true)
            return new ResponseFormat<TaskStatus?>
            {
                Success = false,
                Message = "La compañía ya tiene estatus registrado",
                Data = null
            };

        var taskStatus = new TaskStatus
        {
            StatusName = validName,
            IsActive = 1,
            CompanyId = data.CompanyId,
            CreatedBy = data.CreatedBy
        };

        _conn.TaskStatus.Add(taskStatus);
        await _conn.SaveChangesAsync();

        return new ResponseFormat<TaskStatus?>
            {
                Success = true,
                Message = "Estaus creado con éxito",
                Data = taskStatus
            };
    }

    public async Task<ResponseFormat<TaskStatus?>> EditStatusName(EditTaskStatusDTO data)
    {
        //check if the user exists
        var user = await _us.UserExists(data.CreatedBy);
        if(user == null)
            return new ResponseFormat<TaskStatus?>
            {
                Success = false,
                Message = "El usuario no existe",
                Data = null
            };

        //check if user belongs to company
        var userBelongsToCompany = await _us.UserBelongsToCompany(data.CreatedBy, data.CompanyId); 
        if(userBelongsToCompany == false)
            return new ResponseFormat<TaskStatus?>
            {
                Success = false,
                Message = "El usuario no pertenece a la compañía",
                Data = null
            };

        //check if the stataus exists and belongs to the company
        var validName = data.NewName.Trim();
        validName = validName.ToUpper();

        var sameName = await ExistStatus(validName, data.CompanyId); 
        if(sameName == true)
            return new ResponseFormat<TaskStatus?>
            {
                Success = false,
                Message = "Este es el nombre actual del status",
                Data = null
            };

        var status = await _conn.TaskStatus.FirstOrDefaultAsync(s => s.Id == data.Id);
        if(status == null)
            return new ResponseFormat<TaskStatus?>
            {
                Success = false,
                Message = "No se encontró el estatus " + validName + " en la compañía",
                Data = null
            };
        
        //check if the user that wants to modify the status is the same that created the status 
        if(user.Data != null)
            if(AllowedToModify(data.CreatedBy, user.Data.Id) == false)
                return new ResponseFormat<TaskStatus?>
                {
                    Success = false,
                    Message = "Solo el creador del estatus puede cambiarlo",
                    Data = null
                };

        //update the status
        status.StatusName = validName;
        await _conn.SaveChangesAsync();

        return new ResponseFormat<TaskStatus?>
        {
            Success = true,
            Message = "Nombre del estatus cambiado correctamente",
            Data = status
        };
    }

    public async Task<ResponseFormat<List<StatusListResponseDTO>?>> GetCompanyTasksStatus(GetStatusesDTO data)
    {
        //check if company exists
        var exists = await _cs.CompanyExist(data.CompanyId);
        if(exists == false)
            return new ResponseFormat<List<StatusListResponseDTO>?>
            {
                Success = false,
                Message = "Esta compañía no existe",
                Data = null
            };
        //check if company has any status
        var statuses = await _conn.TaskStatus.
                            Where(ts => ts.CompanyId == data.CompanyId).
                            Select(ts => new StatusListResponseDTO
                            {
                                StatusId = ts.Id,
                                StatusName = ts.StatusName,
                                UserId = ts.Creator.Id,
                                UserName = ts.Creator.FullName
                            }).ToListAsync()
                            ;
        if(statuses.Count == 0)
            return new ResponseFormat<List<StatusListResponseDTO>?>
            {
                Success = false,
                Message = "La compañía no tiene estatus",
                Data = null
            };

        return new ResponseFormat<List<StatusListResponseDTO>?>
        {
            Success = true,
            Message = "Estatus de la compañía obtenidos correctamente",
            Data = statuses
        };
    }
    //check if the statusname exists in a companyid
    public async Task<bool> ExistStatus(string statusname, int companyId)
    {
        var exists = await _conn.TaskStatus.AnyAsync(t => t.StatusName == statusname && t.CompanyId == companyId);
        return exists;
    }

    public async Task<bool> ExistCompanyStatus(int statusId, int companyId)
    {
        var exists = await _conn.TaskStatus.AnyAsync(t => t.Id == statusId && t.CompanyId == companyId);
        return exists;
    }

    public bool AllowedToModify(int createdby, int userid)
    {
        var allowedToEdit = (createdby == userid) ? true : false;
        return allowedToEdit;
    }
}