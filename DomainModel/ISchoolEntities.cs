using System.Data.Entity;
using System.Threading.Tasks;
using DomainModel.Models;

namespace DomainModel
{
    public interface ISchoolEntities
    {
        DbSet<Student> Students { get; set; }
        DbSet<Grade> Grades { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}