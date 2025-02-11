using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Themes;

public class GetThemesByCourseService
{
    private readonly ThemeRepository _themeRepository;

    public GetThemesByCourseService(ThemeRepository themeRepository)
    {
        _themeRepository = themeRepository;
    }
    
    public async Task<Result<List<ThemeResponse>, Error>> Handle(Guid courseId, CancellationToken cancellationToken = default)
    {
        var themesResult = await _themeRepository.GetByCourseId(courseId, cancellationToken);
        var themes = themesResult.Value;
        
        var response = new List<ThemeResponse>();
        
        foreach (var theme in themes)
        {
            var id = theme.Id;
            var title = theme.Title;
            var text = theme.Text;
            var photo = theme.Photo;
            var number = theme.Number;

            var themeResponse = new ThemeResponse(id, title, text, photo, number);
            response.Add(themeResponse);
        }

        return response;
    }
}