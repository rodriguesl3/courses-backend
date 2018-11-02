using Chama.Courses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.Courses.Application.Interfaces
{
    public interface ISignupCourseApplication
    {
        bool SigningupCourse(Student student);
        bool SigningupCouseAsync(Student student);
        bool CourseIsAvailable(Guid courseId);
    }
}
