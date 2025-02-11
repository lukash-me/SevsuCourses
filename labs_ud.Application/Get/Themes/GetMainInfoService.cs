using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Themes;

public class GetMainInfoService
{
    private readonly ThemeRepository _themeRepository;

    public GetMainInfoService(ThemeRepository themeRepository)
    {
        _themeRepository = themeRepository;
    }
    
    public async Task<Result<MainInfoResponse, Error>> Handle(Guid themeId, CancellationToken cancellationToken = default)
    {
        var themeResult = await _themeRepository.GetById(themeId, cancellationToken);

        if (themeResult.IsFailure)
            return themeResult.Error;
        
        var theme = themeResult.Value;
        
        var title = theme.Title;
        var text = theme.Text;
        var photo = theme.Photo;
        
        var response = new MainInfoResponse(title, text, photo);

        return response;
    }
}

public record MainInfoResponse(
    string Title,
    string? Text,
    string? Photo
    );