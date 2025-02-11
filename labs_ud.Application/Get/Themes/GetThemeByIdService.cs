using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Themes;

public class GetThemeByIdService
{
    private readonly ThemeRepository _themeRepository;

    public GetThemeByIdService(ThemeRepository themeRepository)
    {
        _themeRepository = themeRepository;
    }


    public async Task<Result<ThemeResponse, Error>> Handle(ThemeId themeId, CancellationToken cancellationToken = default)
    {
        var themeResult = await _themeRepository.GetById(themeId, cancellationToken);
        var theme = themeResult.Value;
        
        
        var id = theme.Id;
        var title = theme.Title;
        var text = theme.Text;
        var photo = theme.Photo;
        var number = theme.Number;

        var response = new ThemeResponse(id, title, text, photo, number);

        return response;
    }
}