using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;

namespace labs_ud.Application.Entities;

public class Teacher
{
    public Teacher(
        string fio,
        int experience,
        string phone,
        string email,
        string education,
        string photo,
        string description,
        string login,
        string password)
    {
        Id = TeacherId.NewTeacherId();
        Fio = fio;
        Experience = experience;
        Phone = phone;
        Email = email;
        Education = education;
        Photo = photo;
        Description = description;
        Login = login;
        Password = password;
    }
    public Guid Id { get; set; }
    public string Fio { get; set; }
    public int Experience { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Education { get; set; }
    public string Photo { get; set; }
    public string Description { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public void UpdateMainInfo(
        string fio,
        int experience,
        string email,
        string education,
        string description,
        string photo)
    {
        Fio = fio;
        Experience = experience;
        Email = email;
        Education = education;
        Description = description;
        Photo = photo;
    }
    
    public void UpdatePhone(string phone)
    {
        Phone = phone;
    }
    
    public static Result<Teacher, Error> Create(
        string fio,
        int experience,
        string phone,
        string email,
        string education,
        string photo,
        string description,
        string login,
        string password
    )
    {
        var teacher = new Teacher(
            fio,
            experience,
            phone,
            email,
            education,
            photo,
            description,
            login,
            password
        );
        
        return teacher;
    }
}