using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;

namespace labs_ud.Application.Entities;

public class Student
{
    public Student(
        Guid groupId,
        string fio,
        bool isExpelled,
        string photo,
        bool isAttest,
        string phone,
        string email,
        string login,
        string password)
    {
        Id = StudentId.NewStudentId();
        GroupId = groupId;
        Fio = fio;
        IsExpelled = isExpelled;
        Photo = photo;
        IsAttest = isAttest;
        Phone = phone;
        Email = email;
        Login = login;
        Password = password;
    }

    public Guid Id { get; set; }
    public Guid GroupId { get; set; }
    public string Fio { get; set; }
    public bool IsExpelled { get; set; }
    public string Photo { get; set; }
    public bool IsAttest { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public void UpdateMainInfo(string fio, string photo, string email)
    {
        Fio = fio;
        Photo = photo;
        Email = email;
    }
    
    public void UpdatePhone(string phone)
    {
        Phone = phone;
    }
    
    public static Result<Student, Error> Create(
        Guid groupId,
        string fio,
        bool isExpelled,
        string photo,
        bool isAttest,
        string phone,
        string email,
        string login,
        string password
    )
    {
        var student = new Student(
            groupId,
            fio,
            isExpelled,
            photo,
            isAttest,
            phone,
            email,
            login,
            password
        );
        
        return student;
    }
}