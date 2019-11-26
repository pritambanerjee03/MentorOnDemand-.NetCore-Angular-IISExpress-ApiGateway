using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOD.DtoLibrary;
using MOD.MentorLibrary.Repositories;
using MOD.ModelLibrary;

namespace MOD.MentorService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorController : ControllerBase
    {
        IMentorRepository repository;
        public MentorController(IMentorRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet("mentorProfile/{email}")]
        [Authorize(Roles = "Mentor")]
        public IActionResult mentorProfileDetails(string email)
        {
            var result = repository.mentorProfileDetails(email);
            return Ok(result);
        }

        [HttpPut("mentorProfile/{mentorId}")]
        [Authorize(Roles = "Mentor")]
        public IActionResult UpdateMentorDetails(string mentorId, [FromBody] ProfileDto mentorData)
        {
            if (ModelState.IsValid)
            {
                bool result = repository.UpdateMentorDetails(mentorData, mentorId);
                if (result)
                {
                    return Created("UpdatedProfie", null);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet("ListOfCourseMentor/{modelEmail}")]
        [Authorize(Roles = "Mentor")]
        public IActionResult GetEnrolledCoursesByMentor(string modelEmail)
        {
            var result = repository.GetEnrolledCoursesByMentor(modelEmail);
            return Ok(result);
        }
        [HttpPut("ChangeEnrolledCourseStatus/{id}/{UserEmail}")]
        [Authorize(Roles = "Mentor")]
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