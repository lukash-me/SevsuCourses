using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;

namespace labs_ud.Application.Entities;

public class Mentor
{
    public Mentor(
        string fio,
        string phone,
        string email,
        string education,
        string photo,
        string description,
        string password)
    {
        Id = MentorId.NewMentorId();
        Fio = fio;
        Phone = phone;
        Email = email;
        Education = education;
        Photo = photo;
        Description = description;
        Password = password;
    }

    public Guid Id { get; set; }
    public string Fio { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Education { get; set; }
    public string Photo { get; set; }
    public string Description { get; set; }
    public string Password { get; set; }

    public void UpdateMainInfo(
        string fio,
        string email,
        string education,
        string photo,
        string description)
    {
        Fio = fio;
        Email = email;
        Education = education;
        Photo = photo;
        Description = description;
    }
    
    public void UpdatePhone(string phone)
    {
        Phone = phone;
    }
    
    public static Result<Mentor, Error> Create(
        string fio,
        string phone,
        string email,
        string education,
        string photo,
        string description,
        string password
    )
    {
        var mentor = new Mentor(
            fio,
            phone,
            email,
            education,
            photo,
            description,
            password
        );
        
        return mentor;
    }
}