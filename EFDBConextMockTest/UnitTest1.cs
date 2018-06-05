using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using ApplicationService;
using DomainModel;
using DomainModel.Models;
using EFDBContextMockTest.Utility;
using EFDBContextMockTest.Utility.Async;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

// data properties in context must delcare as virtual
namespace EFDBContextMockTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddStudent()
        {
            var mockSet = new Mock<DbSet<Student>>();

            var mockContext = new Mock<SchoolEntities>();
            mockContext.Setup(m => m.Students).Returns(mockSet.Object);

            var repository = new StudentRepository(mockContext.Object);

            var service = new StudentService(repository);
            service.AddStudent(new Student
            {
                Grade = new Grade
                {
                    Id = 1
                },
                Name = "Susen"
            });

            mockSet.Verify(m => m.Add(It.IsAny<Student>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void TestAllStudentsOrderByName()
        {
            var data = new List<Student>
            {
                new Student {Name = "King"},
                new Student {Name = "Ada"},
                new Student {Name = "Zoy"},
                new Student {Name = "Mark"},
            };

            var mockContext = new Mock<SchoolEntities>();
            mockContext.Setup(c => c.Students).ReturnDbSet(data);

            var repository = new StudentRepository(mockContext.Object);
            var service = new StudentService(repository);

            var students = service.GetAllStudents();

            Assert.AreEqual(4, students.Count);
            Assert.AreEqual("Ada", students[0].Name);
            Assert.AreEqual("King", students[1].Name);
            Assert.AreEqual("Mark", students[2].Name);
            Assert.AreEqual("Zoy", students[3].Name);
        }

        [TestMethod]
        public void TestAllStudentsAsyncOrderByName()
        {
            var data = new List<Student>
            {
                new Student {Name = "King"},
                new Student {Name = "Ada"},
                new Student {Name = "Zoy"},
                new Student {Name = "Mark"},
            };

            var mockContext = new Mock<SchoolEntities>();
            mockContext.Setup(c => c.Students).ReturnDbSetAsync(data);

            var repository = new StudentRepository(mockContext.Object);
            var service = new StudentService(repository);

            var students = service.GetAllStudentsAsync().Result;

            Assert.AreEqual(4, students.Count);
            Assert.AreEqual("Ada", students[0].Name);
            Assert.AreEqual("King", students[1].Name);
            Assert.AreEqual("Mark", students[2].Name);
            Assert.AreEqual("Zoy", students[3].Name);
        }
    }
}