using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;

namespace labs_ud.Application.Entities;

public class Theme
{
    public Theme(
        Guid courseId,
        string title,
        string? text,
        string? photo,
        int number)
    {
        Id = ThemeId.NewThemeId();
        CourseId = courseId;
        Title = title;
        Text = text;
        Photo = photo;
        Number = number;
    }

    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Title { get; set; }
    public string? Text { get; set; }
    public string? Photo { get; set; }
    public int Number { get; set; }

    public void UpdateMainInfo(string title, string? text, string? photo)
    {
        Title = title;
        Text = text;
        Photo = photo;
    }
    
    public static Result<Theme, Error> Create(
        Guid courseId,
        string title,
        string? text,
        string? photo,
        int number
    )
    {
        var theme = new Theme(
            courseId,
            title,
            text,
            photo,
            number
        );
        
        return theme;
    }
}