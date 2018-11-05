using Chama.Courses.Domain.Entities;
using Chama.Courses.Persistence.Context;
using Chama.Courses.Persistence.Repository.Interfaces;
using Microsoft.ApplicationInsights;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

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
                QueryCourseCalculate(student);

                return true;
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                return false;
            }
        }
        
        public Course GetDetailCourse(Guid courseId)
        {
            try
            {
                //SELECT * FROM COURSES WITH(NOLOCK) ...
                //https://www.hanselman.com/blog/GettingLINQToSQLAndLINQToEntitiesToUseNOLOCK.aspx
                Course resultList = new Course();
                using (var t = new TransactionScope(TransactionScopeOption.Required,
                                                    new TransactionOptions
                                                    {
                                                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted
                                                    }))
                {
                    resultList = _db.Courses.Where(x => x.Id == courseId)
                       .Include(x => x.Teacher)
                       .Include(x => x.Students)
                       .FirstOrDefault();
                }

                return resultList;
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                throw ex;
            }

        }

        public IEnumerable<Course> GetListCourse()
        {
            try
            {
                var result = _db.Courses.Include(x=>x.Students)
                                        .ToList();

                return result;
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
                throw ex;
            }
        }
        
        private void QueryCourseCalculate(Student student)
        {
            try
            {
                var course = _db.Courses.FirstOrDefault(x => x.Id == student.CourseId);

                if (student.Age <= (course.MinimumAge == 0 ? int.MaxValue : course.MinimumAge))//case courseQuery is empty
                {
                    course.MinimumAge = student.Age;
                }

                if (student.Age >= course.MaximumAge)
                {
                    course.MaximumAge = student.Age;
                }

                course.AverageAge = course.Students.Any() ? course.Students.Average(x => x.Age) : student.Age;
                
                _db.SaveChanges();
                
            }
            catch (Exception ex)
            {
                _telemetry.TrackException(ex);
            }
        }

    }
}
