using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;

namespace labs_ud.Application.Entities;

public class Course
{
    public Course(
        Guid teacherId,
        string title,
        string? description,
        string? photo
        )
    {
        Id = CourseID.NewCourseId();
        TeacherId = teacherId;
        Title = title;
        Description = description;
        Photo = photo;
    }
    public Guid Id { get; set ; }
    public Guid TeacherId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Photo { get; set; }

    public void UpdateMainInfo(string title, string? description, string? photo)
    {
        Title = title;
        Description = description;
        Photo = photo;
    }
    
    public static Result<Course, Error> Create(
        Guid teacherId,
        string title,
        string? description,
        string? photo
        )
    {
        var course = new Course(
            teacherId,
            title,
            description,
            photo
            );
        
        return course;
    }
}