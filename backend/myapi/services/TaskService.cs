using Microsoft.EntityFrameworkCore;

public class TaskService(AppDBConection _conn, CompanyService _cs, TaskStatusService _tss, UserService _uss)
{
    public async Task<ResponseFormat<Task?>> CreateTask(CreateTaskDTO data)
    {
        //check if company exists
        var companyExists = await _cs.CompanyExist(data.CompanyId);
        if(companyExists == false)
            return new ResponseFormat<Task?>{
                Success = false,
                Message = "La compañía no existe",
                Data = null
            };

        //check if the company has that task status
        var statusExists = await _tss.ExistCompanyStatus(data.StatusId, data.CompanyId);
        if(statusExists == false)
            return new ResponseFormat<Task?>{
                Success = false,
                Message = "No existe este estatus en esta compañía",
                Data = null
            };

        //check if the user that creates the task AND the user who is asigned the task belongs to the company
        var taskCreatorBelongs = await _uss.UserBelongsToCompany(data.UserId, data.CompanyId);
        var taskUserExists = await _uss.UserBelongsToCompany(data.UserId, data.CompanyId);
        if(taskCreatorBelongs == false || taskUserExists == false)
        {

            return new ResponseFormat<Task?>{
                Success = false,
                Message = "El creador o el usuario al que se le asignará la tarea no pertenece a la compañía",
                Data = null
            };
        }

        var task = new Task
        {
            Title = data.Title,
            TaskDescription = data.TaskDescription,
            CompanyId = data.CompanyId,
            StatusId = data.StatusId,
            IsPriority = data.IsPriority,
            DueDate = data.DueDate,
            CreatedBy = data.CreatedBy,
            UserId = data.UserId
        };
        
        await _conn.AddAsync(task);
        await _conn.SaveChangesAsync();
        return new ResponseFormat<Task?>{
            Success = true,
            Message = "Task creado correctamente",
            Data = task
        };
    }

    public async Task<ResponseFormat<List<CompanyTaskListResponseDTO>?>> GetCompanyTasks(GetTaskListDTO data)
    {
        //check if company exists
        var companyExists = await _cs.CompanyExist(data.CompanyId);
        if(companyExists == false)
            return new ResponseFormat<List<CompanyTaskListResponseDTO>?>{
                Success = false,
                Message = "La compañía no existe",
                Data = null
            };

        var companyTasks = await _conn.Tasks.Where(t => t.CompanyId == data.CompanyId).
                                            Select(t => new CompanyTaskListResponseDTO
                                            {
                                                Id = t.StatusId,
                                                Title = t.Title,
                                                TaskDescription  = t.TaskDescription,
                                                CompanyId  = t.CompanyId,
                                                CompanyName  = t.Company.CompanyName,
                                                StatusId  = t.StatusId,
                                                StatusName  = t.TaskStatus.StatusName,
                                                IsPriority = t.IsPriority,
                                                DueDate = t.DueDate,
                                                CreatedBy = t.CreatedBy,
                                                CreatedByName  = t.Creator.FullName,
                                                UserId = t.UserId,
                                                UserName  = t.AssignedUser.FullName
                                            }).ToListAsync();

        if(companyTasks.Count < 0 || companyTasks.Count == 0)
            return new ResponseFormat<List<CompanyTaskListResponseDTO>?>{
                Success = false,
                Message = "La compañía no tiene tasks",
                Data = null
            };

        return new ResponseFormat<List<CompanyTaskListResponseDTO>?>{
                Success = true,
                Message = "Tasks de la compañía consultados correctamente",
                Data = companyTasks
            };
    }
}