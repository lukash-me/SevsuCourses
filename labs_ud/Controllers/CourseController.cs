using labs_ud.Application.Create;
using labs_ud.Application.Delete;
using labs_ud.Application.Get;
using labs_ud.Application.IDs;
using labs_ud.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace labs_ud.Controllers;

[ApiController]
[Route("[controller]")]

public class CourseController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateCourse(
        [FromServices] CreateCourseService service,
        [FromBody] CreateCourseRequest request,
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
        [FromServices] DeleteCourseService service,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteCourseRequest(id);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpGet]
    public async Task<ActionResult<Guid>> GetAll(
        [FromServices] GetAllCoursesService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Guid>> GetById(
        [FromRoute] Guid id,
        [FromServices] GetCourseByIdService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(id, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}