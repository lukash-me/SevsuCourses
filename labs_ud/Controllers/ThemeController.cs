using labs_ud.Application.Create;
using labs_ud.Application.Delete;
using labs_ud.Application.Get;
using labs_ud.Application.Get.Themes;
using labs_ud.Application.Update.Theme;
using labs_ud.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace labs_ud.Controllers;

[ApiController]
[Route("[controller]")]

public class ThemeController : ControllerBase
{
    /// <summary>
    /// Создание темы по id курса
    /// </summary>
    /// <param name="allThemesService"></param>
    /// <param name="createThemeService"></param>
    /// <param name="courseId"></param>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{courseId:guid}")]
    public async Task<ActionResult<Guid>> CreateTheme(
        [FromServices] GetThemesByCourseService allThemesService,
        [FromServices] CreateThemeService createThemeService,
        [FromRoute] Guid courseId,
        [FromBody] CreateThemeDto dto,
        CancellationToken cancellationToken = default)
    {
        var themesResult = await allThemesService.Handle(courseId, cancellationToken);

        if (themesResult.IsFailure)
            return themesResult.Error.ToResponse();
        
        var themes = themesResult.Value;
        var number = themes.Count;

        var request = new CreateThemeRequest(courseId, number, dto);
        
        var result = await createThemeService.Handle(request, cancellationToken);
        
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
    
    /// <summary>
    /// Получить основную информацию о теме по id темы
    /// </summary>
    /// <param name="themeId"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("main-info/{themeId:guid}")]
    public async Task<ActionResult<Guid>> GetMainInfo(
        [FromRoute] Guid themeId,
        [FromServices] GetMainInfoService service,
        CancellationToken cancellationToken = default)
    {
        var result = await service.Handle(themeId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    /// <summary>
    /// Изменение основной информации о теме по id темы
    /// </summary>
    /// <param name="themeId"></param>
    /// <param name="dto"></param>
    /// <param name="service"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("main-info/{themeId:guid}")]
    public async Task<ActionResult<Guid>> UpdateMainInfo(
        [FromRoute] Guid themeId,
        [FromBody] MainInfoDto dto,
        [FromServices] UpdateMainInfoService service,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateMainInfoRequest(themeId, dto);

        var result = await service.Handle(request, cancellationToken);

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