using Chama.Courses.Application.Interfaces;
using Chama.Courses.Domain.Entities;
using Chama.Courses.Persistence.Repository.Interfaces;
using System;

namespace Chama.Courses.Application.Implementations
{
    public class SignupCourseApplication : ISignupCourseApplication
    {

        private readonly ISignupCourseRepository _signupRepo;

        public SignupCourseApplication(ISignupCourseRepository signupRepo)
        {
            _signupRepo = signupRepo;
        }

        public bool CourseIsAvailable(Guid courseId)
        {
            return _signupRepo.CourseIsAvailable(courseId);
        }

        public bool SigningupCourse(Student student)
        {
            if (_signupRepo.CourseIsAvailable(student.CourseId))
            {
                return _signupRepo.SigningupCourse(student);
            }

            return false;            
        }
    }
}
