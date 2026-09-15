using FirstApi.Models;

namespace FirstApi.Services;

public interface IStudentService
{
    Task<List<Student>> GetAllStudentsAsync();
    Task<Student?> GetStudentByIdAsync(int id);
    Task<Student> CreateStudentAsync(string name, int age);
    Task <Student?> UpdateStudentAsync(int id, string name, int age);
    Task<bool> DeleteStudentAsync(int id);
}