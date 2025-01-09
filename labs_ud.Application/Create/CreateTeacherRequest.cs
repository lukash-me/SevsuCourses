namespace labs_ud.Application.Create;

public record CreateTeacherRequest(
    string Fio,
    int Experience,
    string Phone,
    string Email,
    string Education,
    string Photo,
    string Description,
    string Login,
    string Password
);