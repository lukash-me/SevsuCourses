using labs_ud.Application.Create;
using labs_ud.Application.Delete;
using labs_ud.Application.Get.Mentor;
using labs_ud.Application.Update.Mentor;
using labs_ud.Application.Update.Share;
using labs_ud.Extensions;
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
    
    /// <summary>
    /// Получить id ментора по логину и паролю
    /// </summary>
    /// <param name="service"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Получение основной информации о менторе по id ментора
    /// </summary>
    /// <param name="mentorId"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("main-info/{mentorId:guid}")]
    public async Task<ActionResult<Guid>> GetMainInfo(
        [FromRoute] Guid mentorId,
        [FromServices] GetMainInfoService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(mentorId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получить номер телефона по id ментора
    /// </summary>
    /// <param name="mentorId"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("phone/{mentorId:guid}")]
    public async Task<ActionResult<Guid>> GetPhone(
        [FromRoute] Guid mentorId,
        [FromServices] GetPhoneService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(mentorId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получение id, фио, всех менторов
    /// </summary>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<ActionResult<Guid>> GetAll(
        [FromServices] GetAllService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Изменить основную информацию о менторе по id ментора
    /// </summary>
    /// <param name="mentorId"></param>
    /// <param name="dto"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("main-info/{mentorId:guid}")]
    public async Task<ActionResult<Guid>> UpdateMainInfo(
        [FromRoute] Guid mentorId,
        [FromBody] MainInfoDto dto,
        [FromServices] UpdateMainInfoService service,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateMainInfoRequest(mentorId, dto);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Изменение номера телефона ментора по id ментора
    /// </summary>
    /// <param name="mentorId"></param>
    /// <param name="dto"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("phone/{mentorId:guid}")]
    public async Task<ActionResult<Guid>> UpdatePhone(
        [FromRoute] Guid mentorId,
        [FromBody] PhoneDto dto,
        [FromServices] UpdatePhoneService service,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdatePhoneRequest(mentorId, dto);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Изменение пароля ментора по id ментора
    /// </summary>
    /// <param name="mentorId"></param>
    /// <param name="dto"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("password/{mentorId:guid}")]
    public async Task<ActionResult<Guid>> UpdatePassword(
        [FromRoute] Guid mentorId,
        [FromBody] PasswordDto dto,
        [FromServices] UpdatePasswordService service,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdatePasswordRequest(mentorId, dto);

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