using FirstApi.Data;
using FirstApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Services;

public class StudentService : IStudentService
{
    private readonly ApplicationDbContext db;
    public StudentService(ApplicationDbContext db)
    {
        this.db = db;
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await db.Students
                        .OrderBy(s => s.Id)
                        .ToListAsync();
    }

    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        return await db.Students.FindAsync(id);
    }

    public async Task<Student> CreateStudentAsync(string name, int age)
    {
        Student student = new Student
        {
            Name = name,
            Age = age
        };

        await db.Students.AddAsync(student);
        await db.SaveChangesAsync();

        return student;
    }

    public async Task<Student?> UpdateStudentAsync(int id, string name, int age)
    {
        Student? student = await db.Students.FindAsync(id);

        if(student == null)
        {
            return null;
        }

        student.Name = name;
        student.Age = age;

        await db.SaveChangesAsync();

        return student;
    }

    public async Task<bool> DeleteStudentAsync(int id)
    {
        Student? student = await db.Students.FindAsync(id);

        if (student == null)
        {
            return false;
        }

        db.Students.Remove(student);
        await db.SaveChangesAsync();

        return true;
    }
}
