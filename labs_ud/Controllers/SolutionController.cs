using labs_ud.Application.Create;
using labs_ud.Application.Delete;
using labs_ud.Application.Get.Solution;
using labs_ud.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace labs_ud.Controllers;

[ApiController]
[Route("[controller]")]

public class SolutionController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateSolution(
        [FromServices] CreateSolutionService service,
        [FromBody] CreateSolutionRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получение эталонных ответов для задачи по id задачи
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("answer/{taskId:guid}")]
    public async Task<ActionResult<Guid>> GetAnswer(
        [FromRoute] Guid taskId,
        [FromServices] GetAnswerService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(taskId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteSolutionService service,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteSolutionRequest(id);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}