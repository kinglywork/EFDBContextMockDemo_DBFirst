using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationService;
using DomainModel;
using DomainModel.Models;

namespace EFDBContextMockDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            var studentService = new StudentService(new StudentRepository(new SchoolEntities()));
            var students = studentService.GetAllStudents();
            
            Console.ReadLine();

        }
    }
}
