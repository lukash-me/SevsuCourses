using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Themes;

public class GetAllThemesIdTitlesByCourseIdService
{
    private readonly ThemeRepository _themeRepository;

    public GetAllThemesIdTitlesByCourseIdService(ThemeRepository themeRepository)
    {
        _themeRepository = themeRepository;
    }
    
    public async Task<Result<List<ThemesResponse>, Error>> Handle(Guid courseId, CancellationToken cancellationToken = default)
    {
        var themesResult = await _themeRepository.GetByCourseId(courseId, cancellationToken);
        var themes = themesResult.Value;
        
        var response = new List<ThemesResponse>();
        
        foreach (var theme in themes)
        {
            var id = theme.Id;
            var title = theme.Title;
            var number = theme.Number;

            var themeResponse = new ThemesResponse(id, title, number);
            response.Add(themeResponse);
        }
        
        response = response.OrderBy(t => t.Number).ToList();

        return response;
    }
}

public record ThemesResponse(
    Guid Id,
    string Title,
    int Number
    );