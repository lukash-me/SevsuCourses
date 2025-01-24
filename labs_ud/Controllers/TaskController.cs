using labs_ud.Application.Create;
using labs_ud.Application.Delete;
using labs_ud.Application.Get.Tasks;
using labs_ud.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace labs_ud.Controllers;

[ApiController]
[Route("[controller]")]

public class TaskController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateTask(
        [FromServices] CreateTaskService service,
        [FromBody] CreateTaskRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteTaskService service,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteTaskRequest(id);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получить id и названия задач по id темы
    /// </summary>
    /// <param name="service"></param>
    /// <param name="themeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("theme/{themeId:guid}")]
    public async Task<ActionResult<Guid>> GetAllByTheme(
        [FromServices] GetTasksByThemeService service,
        [FromRoute] Guid themeId,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(themeId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Guid>> GetById(
        [FromRoute] Guid id,
        [FromServices] GetTaskByIdService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(id, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}