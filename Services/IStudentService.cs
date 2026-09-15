using FirstApi.Models;

namespace FirstApi.Services;

public interface IStudentService
{
    Task<List<Student>> GetAllStudentsAsync();
    Task<Student?> GetStudentByIdAsync(int id);
    Student CreateStudent(string name, int age);
    Student? UpdateStudent(int id, string name, int age);
    bool DeleteStudent(int id);
}