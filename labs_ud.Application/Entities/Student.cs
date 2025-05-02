using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using labs_ud.Application.Entities.Validation;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;

namespace labs_ud.Application.Entities;

public class Student
{
    public Student(
        string fio,
        bool isExpelled,
        string photo,
        bool isAttest,
        string phone,
        string email,
        string password)
    {
        Id = StudentId.NewStudentId();
        Fio = fio;
        IsExpelled = isExpelled;
        Photo = photo;
        IsAttest = isAttest;
        Phone = phone;
        Email = email;
        Password = password;
    }

    public Guid Id { get; set; }
    public string Fio { get; set; }
    public bool IsExpelled { get; set; }
    public string Photo { get; set; }
    public bool IsAttest { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public Result<string, Error> UpdateMainInfo(string fio, string photo, string email)
    {
        var validationMainInfoResult = User.CheckMainInfo(fio, email, Phone, photo);
        if (validationMainInfoResult.IsFailure)
        {
            return validationMainInfoResult.Error;
        }
        
        Fio = fio;
        Photo = photo;
        Email = email;

        return "Success";
    }
    
    public Result<string, Error> UpdatePassword(string password)
    {
        var validationPasswordResult = User.CheckPassword(password);
        if (validationPasswordResult.IsFailure)
        {
            return validationPasswordResult.Error;
        }
        
        Password = password;

        return "Success";
    }
    
    public Result<string, Error> UpdatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return Errors.Errors.General.ValueIsRequired("phone");
        }

        if (Regex.IsMatch(phone, Constants.Regexes.RU_PHONE_REGEX) == false)
        {
            return Errors.Errors.General.ValueIsInvalid("phone");
        }
        
        Phone = phone;
        
        return "Success";
    }
    
    public static Result<Student, Error> Create(
        string fio,
        bool isExpelled,
        string photo,
        bool isAttest,
        string phone,
        string email,
        string password
    )
    {
        var validationMainInfoResult = User.CheckMainInfo(fio, email, phone, photo);
        if (validationMainInfoResult.IsFailure)
        {
            return validationMainInfoResult.Error;
        }

        var validationPasswordResult = User.CheckPassword(password);
        if (validationPasswordResult.IsFailure)
        {
            return validationPasswordResult.Error;
        }
        
        var student = new Student(
            fio,
            isExpelled,
            photo,
            isAttest,
            phone,
            email,
            password
        );
        
        return student;
    }
}