using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("task_status")]
public class TaskStatusController(TaskStatusService _tss) : ControllerBase
{
    //Authorize because we need a valid token to accecss this endpoint
    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateTaskStatusDTO data)
    {
        //task status and createdby cannot be bull
        if(data.StatusName == "" || data.CreatedBy < 0 || data.CreatedBy == 0)
            return BadRequest(new ResponseFormat<object>
            {
                Success = false,
                Message = "Información incorrecta",
                Data = null
            });

        var result = await _tss.Create(data);

        if(result.Success == false)
            return BadRequest(result);

        return Ok(result);
    }

    [Authorize]
    [HttpPost("edit")]
    public async Task<IActionResult> Edit([FromBody] EditTaskStatusDTO data)
    {
        var response = await _tss.EditStatusName(data);
        
        if(response.Success == false)
            return BadRequest(response.Message);

        return Ok(response);
    }

    [Authorize]
    [HttpPost("getStatuses")]
    public async Task<IActionResult> GetStatuses([FromBody] GetStatusesDTO data)
    {

        var response = await _tss.GetCompanyTasksStatus(data);
        if(response.Success == false)
            return BadRequest(response);
        
        return Ok(response);
    }
}