namespace labs_ud.Application.Create;

public record CreateStudentRequest(
    Guid GroupId,
    string Fio,
    bool IsExpelled,
    string Photo,
    bool IsAttest,
    string Phone,
    string Email,
    string Login,
    string Password);