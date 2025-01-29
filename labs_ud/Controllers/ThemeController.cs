using labs_ud.Application.Create;
using labs_ud.Application.Delete;
using labs_ud.Application.Get;
using labs_ud.Application.Get.Themes;
using labs_ud.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace labs_ud.Controllers;

[ApiController]
[Route("[controller]")]

public class ThemeController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateTheme(
        [FromServices] CreateThemeService service,
        [FromBody] CreateThemeRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получить все темы по id курса
    /// </summary>
    /// <param name="service"></param>
    /// <param name="courseId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("all/{courseId:guid}")]
    public async Task<ActionResult<Guid>> GetAllByCourse(
        [FromServices] GetThemesByCourseService service,
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(courseId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Получить все id и названия тем по id курса
    /// </summary>
    /// <param name="service"></param>
    /// <param name="courseId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("all/titles/{courseId:guid}")]
    public async Task<ActionResult<Guid>> GetAllIdTitlesByCourse(
        [FromServices] GetAllThemesIdTitlesByCourseIdService service,
        [FromRoute] Guid courseId,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(courseId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Guid>> GetById(
        [FromRoute] Guid id,
        [FromServices] GetThemeByIdService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(id, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteThemeService service,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteThemeRequest(id);

        var result = await service.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}