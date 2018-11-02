using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chama.Courses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [Route("api/[controller]/verify-signup")]
        [HttpGet]
        public IActionResult Get()
        {
            //TODO: after create DI finish controller
            return Ok();
        }

        [Route("api/[controller]/sign-up")]
        [HttpPost]
        public IActionResult Post()
        {
            //TODO: after create DI finish controller and add parameter fromBody.
            return Ok();
        }
    }
}