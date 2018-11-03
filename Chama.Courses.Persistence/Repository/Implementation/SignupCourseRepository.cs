using Chama.Courses.Domain.Entities;
using Chama.Courses.Persistence.Context;
using Chama.Courses.Persistence.Repository.Interfaces;
using Microsoft.ApplicationInsights;
using System;
using System.Linq;

namespace Chama.Courses.Persistence.Repository.Implementation
{
    public class SignupCourseRepository : ISignupCourseRepository
    {

        private readonly EfDbContext _db;
        private readonly TelemetryClient _telemetry;

        public SignupCourseRepository(EfDbContext db, TelemetryClient telemetry)
        {
            _db = db;
            _telemetry = telemetry;
        }

        public bool CourseIsAvailable(Guid courseId)
        {
            try
            {
                var courseEntity = _db.Courses.FirstOrDefault(x => x.Id == courseId);
                var students = _db.Students.Where(x => x.CourseId == courseId).ToList();
                return courseEntity.MaximumStudents >= (students?.Count ?? 0);
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                return false;
            }
        }
            

        public bool SigningupCourse(Student student)
        {
            try
            {
                _db.Students.Add(student);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                return false;
            }
        }
    }
}
