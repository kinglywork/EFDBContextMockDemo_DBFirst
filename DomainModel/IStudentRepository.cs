using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Models;

namespace DomainModel
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Task<List<Student>> GetAllAsync();

        Student AddStudent(Student student);
    }
}