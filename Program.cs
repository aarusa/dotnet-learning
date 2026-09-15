using FirstApi.Data;
using Microsoft.EntityFrameworkCore;
using FirstApi.Models;
using FirstApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();

app.MapGet("/", () => "My first DOTNET API!");

app.MapGet("/about", () => "This is about page.");

app.MapGet("/students", async (IStudentService studentService) =>
{
    return await studentService.GetAllStudentsAsync();
});

app.MapGet("/students/{id}", async (IStudentService studentService, int id) =>
{
    Student? student = await studentService.GetStudentByIdAsync(id);

    if (student == null)
    {
        return Results.NotFound("Student not found.");
    }

    return Results.Ok(student);
});

app.MapPost("/students", async (IStudentService studentService, CreateStudentRequest request) =>
{
   Student student = await studentService.CreateStudentAsync(
        request.Name,
        request.Age
   );

   return Results.Created($"/students/{student.Id}", student); 
});

app.MapPut("/students/{id}", async (IStudentService studentService, int id, UpdateStudentRequest request) =>
{
    Student? student = await studentService.UpdateStudentAsync(
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

app.MapDelete("/students/{id}", async (IStudentService studentService, int id) =>
{
    bool deleted = await studentService.DeleteStudentAsync(id);

    if(!deleted)
    {
        return Results.NotFound("Student not found.");
    }
    
    return Results.NoContent();
});

app.Run();