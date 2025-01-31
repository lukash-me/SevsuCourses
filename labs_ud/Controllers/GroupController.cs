using labs_ud.Application.Create;
using labs_ud.Application.Delete;
using labs_ud.Application.Get.Group;
using labs_ud.Application.Get.Student;
using labs_ud.Application.Update.Group;
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
    
    /// <summary>
    /// Получение всей информации о группе по id группы
    /// </summary>
    /// <param name="groupId"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{groupId:guid}")]
    public async Task<ActionResult<Guid>> GetById(
        [FromRoute] Guid groupId,
        [FromServices] GetGroupByIdService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(groupId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Изменить ментора для группы по id группы и id ментора
    /// </summary>
    /// <param name="groupId"></param>
    /// <param name="dto"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{groupId:guid}")]
    public async Task<ActionResult<Guid>> UpdateMentor(
        [FromRoute] Guid groupId,
        [FromBody] UpdateMentorDto dto,
        [FromServices] UpdateMentorService service,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateMentorRequest(groupId, dto);

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