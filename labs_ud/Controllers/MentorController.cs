using labs_ud.Application.Create;
using labs_ud.Application.Delete;
using labs_ud.Application.Get.Mentor;
using labs_ud.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace labs_ud.Controllers;

[ApiController]
[Route("[controller]")]

public class MentorController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateMentor(
        [FromServices] CreateMentorService service,
        [FromBody] CreateMentorRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<Guid>> GetByLoginPassword(
        [FromServices] GetMentorByLoginPasswordService service,
        [FromBody] MentorRequest request,
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
        [FromServices] DeleteMentorService service,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteMentorRequest(id);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}