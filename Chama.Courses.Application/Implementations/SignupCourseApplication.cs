using Chama.Courses.Application.Interfaces;
using Chama.Courses.Domain.Configuration;
using Chama.Courses.Domain.Entities;
using Chama.Courses.Infrastructure.Interfaces;
using Chama.Courses.Infrastructure.ServiceBus;
using Chama.Courses.Persistence.Repository.Interfaces;
using System;
using System.Collections.Generic;
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

       

        public Course GetDetailCourse(Guid courseId)
        {
            try
            {
                return _signupRepo.GetDetailCourse(courseId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Course> GetListCourse()
        {
            try
            {
                return _signupRepo.GetListCourse();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool SigningupCourse(Student student)
        {
            try
            {
                if (_signupRepo.CourseIsAvailable(student.CourseId))
                {
                    return _signupRepo.SigningupCourse(student);
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool>SigningupCourseAsync(Student student, ServiceBusConfiguration serviceBusConfig)
        {
            try
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
