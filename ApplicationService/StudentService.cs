using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel;
using DomainModel.Models;

namespace ApplicationService
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public List<Student> GetAllStudents()
        {
            return _studentRepository.GetAll();
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public Student AddStudent(Student student)
        {
            return _studentRepository.AddStudent(student);
        }
    }
}