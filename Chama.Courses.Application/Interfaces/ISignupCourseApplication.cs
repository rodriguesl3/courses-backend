using Chama.Courses.Domain.Configuration;
using Chama.Courses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chama.Courses.Application.Interfaces
{
    public interface ISignupCourseApplication
    {
        bool SigningupCourse(Student student);
        Task<bool> SigningupCourseAsync(Student student, ServiceBusConfiguration serviceBusConfig);
        bool CourseIsAvailable(Guid courseId);
        Course GetDetailCourse(Guid courseId);
        IEnumerable<Course> GetListCourse();
    }
}
