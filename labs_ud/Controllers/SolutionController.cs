using labs_ud.Application.Create;
using labs_ud.Application.Delete;
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