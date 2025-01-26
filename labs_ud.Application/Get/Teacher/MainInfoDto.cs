namespace labs_ud.Application.Get.Teacher;

public record MainInfoDto(
    string Fio,
    int Experience,
    string Email,
    string Education,
    string Description,
    string Photo
);