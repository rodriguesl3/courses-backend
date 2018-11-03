using Chama.Courses.Application.Interfaces;
using Chama.Courses.Domain.Configuration;
using Chama.Courses.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Chama.Courses.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {

        private readonly string successfull = "You will receive an email, case if got it or not";
        private readonly string fullCourse = "Oh no! Our course is full, do you mind choose another one?";


        [Route("api/[controller]/verify-signup/{id}")]
        [HttpGet]
        public IActionResult Get([FromServices] ISignupCourseApplication _signupApp, Guid id)
        {
            return Ok(_signupApp.CourseIsAvailable(id));
        }

        [Route("api/[controller]/sign-up")]
        [HttpPost]
        public IActionResult Post([FromServices] ISignupCourseApplication _signupApp, Student student)
        {
            _signupApp.SigningupCourse(student);
            return Ok();
        }


        [Route("api/[controller]/sign-up-async")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromServices] ISignupCourseApplication _signupApp,
                                       [FromServices] ServiceBusConfiguration serviceBusConfig,
                                        Student student)
        {

            if (student.Id == Guid.Empty)
            {
                student.Id = Guid.NewGuid();
            }

            var returnMessage = _signupApp.SigningupCourseAsync(student, serviceBusConfig).GetAwaiter().GetResult();
            return Ok(returnMessage ? successfull : fullCourse);
        }




    }
}