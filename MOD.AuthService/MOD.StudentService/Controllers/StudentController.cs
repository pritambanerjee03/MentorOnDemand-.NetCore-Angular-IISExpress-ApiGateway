using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOD.DtoLibrary;
using MOD.ModelLibrary;
using MOD.StudentLibrary.Repositories;

namespace MOD.StudentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IStudentRepository repository;
        public StudentController(IStudentRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("studentProfile/{email}")]
        [Authorize(Roles = "Student")]
        public IActionResult studentProfileDetails(string email)
        {
            var result = repository.studentProfileDetails(email);
            return Ok(result);
        }

        [HttpPut("studentProfile/{studentId}")]
        [Authorize(Roles = "Student")]
        public IActionResult UpdateStudentDetails(string studentId, [FromBody] ProfileDto studentData)
        {
            if (ModelState.IsValid)
            {
                bool result = repository.UpdateStudentDetails(studentData, studentId);
                if (result)
                {
                    return Created("UpdatedProfie", null);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet("ListOfCourse/{modelEmail}")]
        [Authorize(Roles = "Student")]
        public IActionResult GetEnrolledCoursesByStudent(string modelEmail)
        {
            var result = repository.GetEnrolledCoursesByStudent(modelEmail);
            return Ok(result);
        }
        [HttpPost]
        [Authorize(Roles = "Student")]
        public IActionResult Post([FromBody] EnrolledCourse enrolledCourse)
        {
            if (ModelState.IsValid)
            {

                bool result = repository.AddEnrolledCourses(enrolledCourse);
                if (result)
                {
                    return Created("AddCoursesEnrolled", enrolledCourse);
                }
                return BadRequest(new { Message = "You have already Enrolled for This Course." });

                //return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("ChangeEnrolledCourseStatus/{id}/{UserEmail}")]
        [Authorize(Roles = "Student")]
        public IActionResult ChangeCourseStatus(int id, string UserEmail, [FromBody] EnrolledCourse enrolledCourse)
        {
            if (ModelState.IsValid && id == enrolledCourse.Id)
            {
                bool result = repository.ChangeCourseStatus(enrolledCourse, UserEmail);
                if (result)
                {
                    return Created("UpdatedCourse", enrolledCourse.Id);
                }
            }
            return BadRequest(ModelState);
        }

    }
}