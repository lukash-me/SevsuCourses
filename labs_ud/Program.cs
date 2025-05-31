using labs_ud.Application.Create;
using labs_ud.Application.Create.StudentGroup;
using labs_ud.Application.DataBase;
using labs_ud.Application.Delete;
using labs_ud.Application.Delete.StudentGroup;
using labs_ud.Application.Get.Answer;
using labs_ud.Application.Get.Course;
using labs_ud.Application.Get.Group;
using labs_ud.Application.Get.Mentor;
using labs_ud.Application.Get.Solution;
using labs_ud.Application.Get.Student;
using labs_ud.Application.Get.StudentGroup;
using labs_ud.Application.Get.Task;
using labs_ud.Application.Get.Teacher;
using labs_ud.Application.Get.Themes;
using labs_ud.Application.Repositories;
using labs_ud.Application.Update.Answer;
using labs_ud.Application.Update.Group;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
    options.AddPolicy("AllowVueApp", builder =>
        builder.WithOrigins("http://localhost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services
    .AddScoped<GetGroupsByCourseId>()
    .AddScoped<GetGroupsByMentorIdAndCourseId>()
    .AddScoped<labs_ud.Application.Update.Teacher.UpdatePhoneService>()
    .AddScoped<labs_ud.Application.Update.Mentor.UpdatePhoneService>()
    .AddScoped<labs_ud.Application.Update.Student.UpdatePhoneService>()
    .AddScoped<labs_ud.Application.Update.Mentor.UpdateMainInfoService>()
    .AddScoped<labs_ud.Application.Update.Student.UpdateMainInfoService>()
    .AddScoped<labs_ud.Application.Update.Teacher.UpdateMainInfoService>()
    .AddScoped<labs_ud.Application.Update.Task.UpdateMainInfoService>()
    .AddScoped<labs_ud.Application.Update.Theme.UpdateMainInfoService>()
    .AddScoped<labs_ud.Application.Update.Course.UpdateMainInfoService>()
    .AddScoped<UpdateReplyAndMarkService>()
    .AddScoped<GetAnswerService>()
    .AddScoped<GetBestAnswersToThemeService>()
    .AddScoped<GetStatusBestMarkDatesByStudentIdTaskIdService>()
    .AddScoped<GetTitlesAndIdsByTeacherIdService>()
    .AddScoped<labs_ud.Application.Get.Teacher.GetPhoneService>()
    .AddScoped<labs_ud.Application.Get.Mentor.GetPhoneService>()
    .AddScoped<labs_ud.Application.Get.Student.GetPhoneService>()
    .AddScoped<labs_ud.Application.Get.Task.GetMainInfoService>()
    .AddScoped<labs_ud.Application.Get.Teacher.GetMainInfoService>()
    .AddScoped<labs_ud.Application.Get.Mentor.GetMainInfoService>()
    .AddScoped<labs_ud.Application.Get.Student.GetMainInfoService>()
    .AddScoped<labs_ud.Application.Get.Themes.GetMainInfoService>()
    .AddScoped<GetMentorFioByIdService>()
    .AddScoped<UpdateMentorService>()
    .AddScoped<labs_ud.Application.Get.Mentor.GetAllService>()
    .AddScoped<labs_ud.Application.Get.Student.GetAllService>()
    .AddScoped<GetTeacherFioByIdService>()
    .AddScoped<GetTitleAndTeacherByIdService>()
    .AddScoped<GetGroupByIdService>()
    .AddScoped<GetGroupsByStudentIdService>()
    .AddScoped<GetStudentsFioStatusByStudentIdService>()
    .AddScoped<GetStudentsByGroupIdService>()
    .AddScoped<CreateStudentGroupService>()
    .AddScoped<CreateBulkService>()
    .AddScoped<DeleteStudentGroupService>()
    .AddScoped<UpdateAnswerTextService>()
    .AddScoped<GetLastAnswerService>()
    .AddScoped<GetAllAnswersByStudentIdTaskIdService>()
    .AddScoped<GetMentorByLoginPasswordService>()
    .AddScoped<GetTeacherByLoginPasswordService>()
    .AddScoped<GetStudentByLoginPasswordService>()
    .AddScoped<labs_ud.Application.Update.Teacher.UpdatePasswordService>()
    .AddScoped<labs_ud.Application.Update.Student.UpdatePasswordService>()
    .AddScoped<labs_ud.Application.Update.Mentor.UpdatePasswordService>()
    .AddScoped<GetThemeByIdService>()
    .AddScoped<GetTaskByIdService>()
    .AddScoped<GetTasksByThemeService>()
    .AddScoped<GetCourseByIdService>()
    .AddScoped<GetThemesByCourseService>()
    .AddScoped<GetAllThemesIdTitlesByCourseIdService>()
    .AddScoped<GetAllCoursesService>()
    .AddScoped<CreateAnswerService>()
    .AddScoped<DeleteAnswerService>()
    .AddScoped<CreateStudentService>()
    .AddScoped<DeleteStudentService>()
    .AddScoped<CreateGroupService>()
    .AddScoped<DeleteGroupService>()
    .AddScoped<CreateSolutionService>()
    .AddScoped<DeleteSolutionService>()
    .AddScoped<CreateTaskService>()
    .AddScoped<DeleteTaskService>()
    .AddScoped<CreateCourseService>()
    .AddScoped<DeleteCourseService>()
    .AddScoped<CreateTeacherService>()
    .AddScoped<DeleteTeacherService>()
    .AddScoped<CreateMentorService>()
    .AddScoped<DeleteMentorService>()
    .AddScoped<CreateThemeService>()
    .AddScoped<DeleteThemeService>()
    .AddScoped<ApplicationDbContext>()
    .AddScoped<CourseRepository>()
    .AddScoped<TeacherRepository>()
    .AddScoped<MentorRepository>()
    .AddScoped<ThemeRepository>()
    .AddScoped<TaskRepository>()
    .AddScoped<SolutionRepository>()
    .AddScoped<GroupRepository>()
    .AddScoped<StudentRepository>()
    .AddScoped<AnswerRepository>()
    .AddScoped<StudentGroupRepository>()
    .AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseCors("AllowAllOrigins");
    app.UseCors("AllowVueApp");
    app.UseRouting();
}

app.UseStaticFiles();
app.MapControllers();

app.Run();