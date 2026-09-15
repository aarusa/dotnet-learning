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
        return await db.Students.ToListAsync();
    }

    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        return await db.Students.FindAsync(id);
    }

    public Student CreateStudent(string name, int age)
    {
        Student student = new Student
        {
            Name = name,
            Age = age
        };

        db.Students.Add(student);
        db.SaveChanges();

        return student;
    }

    public Student? UpdateStudent(int id, string name, int age)
    {
        Student? student = db.Students.Find(id);

        if(student == null)
        {
            return null;
        }

        student.Name = name;
        student.Age = age;

        db.SaveChanges();

        return student;
    }

    public bool DeleteStudent(int id)
    {
        Student? student = db.Students.Find(id);

        if (student == null)
        {
            return false;
        }

        db.Students.Remove(student);
        db.SaveChanges();

        return true;
    }
}
