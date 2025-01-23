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
    
    /// <summary>
    /// Получение ФИО ментора по id ментора
    /// </summary>
    /// <param name="mentorId"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("fio/{mentorId:guid}")]
    public async Task<ActionResult<Guid>> GetFioByTeacherId(
        [FromRoute] Guid mentorId,
        [FromServices] GetMentorFioByIdService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(mentorId, cancellationToken);

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