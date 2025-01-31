namespace labs_ud.Application.Create;

public record CreateStudentRequest(
    string Fio,
    bool IsExpelled,
    string Photo,
    bool IsAttest,
    string Phone,
    string Email,
    string Password);