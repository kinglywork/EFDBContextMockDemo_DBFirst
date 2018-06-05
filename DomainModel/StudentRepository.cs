using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Models;

namespace DomainModel
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ISchoolEntities _entities;

        public StudentRepository(SchoolEntities entities)
        {
            _entities = entities;
        }

        public List<Student> GetAll()
        {
            return _entities.Students.OrderBy(s => s.Name).ToList();
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _entities.Students.OrderBy(s => s.Name).ToListAsync();
        }

        public Student AddStudent(Student student)
        {
            var addedStudent = _entities.Students.Add(new Student()
            {
                Grade = student.Grade,
                Name = student.Name
            });
            _entities.SaveChanges();

            return addedStudent;
        }
    }
}