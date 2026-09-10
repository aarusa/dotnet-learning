using FirstApi.Models;
using FirstApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IStudentService, StudentService>();

var app = builder.Build();

app.MapGet("/", () => "My first DOTNET API!");

app.MapGet("/about", () => "This is about page.");

app.MapGet("/students", (IStudentService studentService) =>
{
    return studentService.GetAllStudents();
});

app.MapGet("/students/{id}", (IStudentService studentService, int id) =>
{
    Student? student = studentService.GetStudentById(id);

    if (student == null)
    {
        return Results.NotFound("Student not found.");
    }

    return Results.Ok(student);
});

app.MapPost("/students", (IStudentService studentService, CreateStudentRequest request) =>
{
   Student student = studentService.CreateStudent(
        request.Name,
        request.Age
   );

   return Results.Created($"/students/{student.Id}", student); 
});

app.MapPut("/students/{id}", (IStudentService studentService, int id, UpdateStudentRequest request) =>
{
    Student? student = studentService.UpdateStudent(
        id,
        request.Name,
        request.Age
    );

    if(student == null)
    {
        return Results.NotFound("Student not found.");
    }

    return Results.Ok(student);
});

app.MapDelete("/students/{id}", (IStudentService studentService, int id) =>
{
    bool deleted = studentService.DeleteStudent(id);

    if(!deleted)
    {
        return Results.NotFound("Student not found.");
    }
    
    return Results.NoContent();
});

app.Run();