using labs_ud.Application.Create;
using labs_ud.Application.Delete;
using labs_ud.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace labs_ud.Controllers;

[ApiController]
[Route("[controller]")]

public class GroupController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateGroup(
        [FromServices] CreateGroupService service,
        [FromBody] CreateGroupRequest request,
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
        [FromServices] DeleteGroupService service,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteGroupRequest(id);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}