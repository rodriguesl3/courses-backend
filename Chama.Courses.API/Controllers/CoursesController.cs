using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chama.Courses.Application.Interfaces;
using Chama.Courses.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chama.Courses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
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
        public IActionResult PostAsync([FromServices] ISignupCourseApplication _signupApp, Student student)
        {
            _signupApp.SigningupCouseAsync(student);
            return Ok();
        }




    }
}