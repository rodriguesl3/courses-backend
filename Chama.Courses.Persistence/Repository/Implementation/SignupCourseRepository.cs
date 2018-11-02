using Chama.Courses.Domain.Entities;
using Chama.Courses.Persistence.Context;
using Chama.Courses.Persistence.Repository.Interfaces;
using System;
using System.Linq;

namespace Chama.Courses.Persistence.Repository.Implementation
{
    public class SignupCourseRepository : ISignupCourseRepository
    {

        private readonly EfDbContext _db;

        public SignupCourseRepository(EfDbContext db)
        {
            _db = db;
        }

        public bool CourseIsAvailable(Guid courseId)
        {
            var courseEntity = _db.Courses.FirstOrDefault(x => x.Id == courseId);
            return courseEntity.MaximumStudents >= (courseEntity.Students?.Count ?? 0);
        }

        public bool SigningupCourse(Student student)
        {
            _db.Students.Add(student);
            _db.SaveChanges();
            return true;
        }
    }
}
