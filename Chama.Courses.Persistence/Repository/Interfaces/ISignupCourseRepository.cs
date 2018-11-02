using Chama.Courses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.Courses.Persistence.Repository.Interfaces
{
    public interface ISignupCourseRepository
    {
        bool SigningupCourse(Student student);
        bool CourseIsAvailable(Guid courseId);
    }
}
