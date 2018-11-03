using Chama.Courses.Application.Interfaces;
using Chama.Courses.Domain.Configuration;
using Chama.Courses.Domain.Entities;
using Chama.Courses.Infrastructure.Interfaces;
using Chama.Courses.Infrastructure.ServiceBus;
using Chama.Courses.Persistence.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace Chama.Courses.Application.Implementations
{
    public class SignupCourseApplication : ISignupCourseApplication
    {

        private readonly ISignupCourseRepository _signupRepo;
        private readonly IServiceBusInfrastructure _serviceBus;

        public SignupCourseApplication(ISignupCourseRepository signupRepo, IServiceBusInfrastructure serviceBus
)
        {
            _signupRepo = signupRepo;
            _serviceBus = serviceBus;
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

        public async Task<bool>SigningupCourseAsync(Student student, ServiceBusConfiguration serviceBusConfig)
        {
            if (_signupRepo.CourseIsAvailable(student.CourseId))
            {
                _serviceBus.SendAsync(student, serviceBusConfig);

                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
