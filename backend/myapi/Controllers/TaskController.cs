using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("tasks")]
public class TaskController(TaskService _ts) : ControllerBase
{
    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDTO data)
    {
        var response = await _ts.CreateTask(data);
        if(response.Success == false)
            return BadRequest(response);

        return Ok(response);
    }

    [Authorize]
    [HttpPost("get_tasks")]
    public async Task<IActionResult> GetTasks([FromBody] GetTaskListDTO data)
    {
        var response = await _ts.GetCompanyTasks(data);
        if(response.Success == false)
            return BadRequest(response);

        return Ok(response);
    }
}