
using Chama.Courses.Application.Implementations;
using Chama.Courses.Application.Interfaces;
using Chama.Courses.Domain.Entities;
using Chama.Courses.Infrastructure.Interfaces;
using Chama.Courses.Persistence.Repository.Interfaces;
using Moq;
using System;
using Xunit;

namespace Chama.Courses.Test
{
    public class Signup
    {
        [Fact]
        public void Course_Is_Not_Available_OK()
        {
            var courseId = Guid.Parse("904ec132-3fc7-4e70-be64-d2efef40bd95");

            var course = new Mock<Course>();
            var appMock = new Mock<ISignupCourseApplication>();
            var repoMock = new Mock<ISignupCourseRepository>();
            var serviceBus = new Mock<IServiceBusInfrastructure>();

            appMock.Setup(x => x.CourseIsAvailable(courseId)).Returns(true);
            var expect = appMock.Object.CourseIsAvailable(courseId);

            var application = new SignupCourseApplication(repoMock.Object, serviceBus.Object);
            var result = application.CourseIsAvailable(courseId);

            Assert.Equal(result, expect);
        }


        [Fact]
        public void Course_Signup_OK()
        {
            var courseId = Guid.NewGuid();
            var student = new Student { CourseId = courseId, Age = 35, Id = Guid.NewGuid(), Name = "Robert" };

            var appMock = new Mock<ISignupCourseApplication>();
            var repoMock = new Mock<ISignupCourseRepository>();
            var serviceBus = new Mock<IServiceBusInfrastructure>();

            appMock.Setup(x => x.CourseIsAvailable(courseId)).Returns(true);
            appMock.Setup(x => x.SigningupCourse(student)).Returns(true);
            
            var expectAvailable = appMock.Object.CourseIsAvailable(courseId);
            var expect = repoMock.Object.SigningupCourse(student);

            var application = new SignupCourseApplication(repoMock.Object, serviceBus.Object);
            var result = application.SigningupCourse(student);

            Assert.Equal(result, expect);
        }




    }
}
