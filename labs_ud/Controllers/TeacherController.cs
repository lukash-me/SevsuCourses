using labs_ud.Application.Create;
using labs_ud.Application.Delete;
using labs_ud.Application.Get.Teacher;
using labs_ud.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace labs_ud.Controllers;

[ApiController]
[Route("[controller]")]

public class TeacherController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateTeacher(
        [FromServices] CreateTeacherService service,
        [FromBody] CreateTeacherRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<Guid>> GetByLoginPassword(
        [FromServices] GetTeacherByLoginPasswordService service,
        [FromBody] TeacherRequest request,
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
        [FromServices] DeleteTeacherService service,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteTeacherRequest(id);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}