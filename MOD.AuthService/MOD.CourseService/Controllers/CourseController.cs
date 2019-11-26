using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOD.CourseLibrary.Repositories;

namespace MOD.CourseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        ICourseRepository repository;
        public CourseController(ICourseRepository repository)
        {
            this.repository = repository;
        }
        // GET: api/Course
        [HttpGet]
        //[Authorize(Roles = "Admin,Student,Mentor")]
        public IActionResult GetCourses()
        {
            return Ok(repository.GetCourses());
        }
        [HttpGet("search/{criteria}")]
        public IActionResult SearchCourse(string criteria)
        {
            var result = repository.SearchCourse(criteria);
            return Ok(result);
        }

    }
}