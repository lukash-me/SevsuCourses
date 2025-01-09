using labs_ud.Application.Create;
using labs_ud.Application.Delete;
using labs_ud.Application.Get.Student;
using labs_ud.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace labs_ud.Controllers;

[ApiController]
[Route("[controller]")]

public class StudentController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateStudent(
        [FromServices] CreateStudentService service,
        [FromBody] CreateStudentRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<Guid>> GetByLoginPassword(
        [FromServices] GetStudentByLoginPasswordService service,
        [FromBody] StudentRequest request,
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
        [FromServices] DeleteStudentService service,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteStudentRequest(id);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    
}