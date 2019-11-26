using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOD.AdminLibrary.Repositories;
using MOD.ModelLibrary;

namespace MOD.AdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IAdminRepository repository;
        public AdminController(IAdminRepository repository)
        {
            this.repository = repository;
        }

        // POST: api/Admin
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post([FromBody] Course course)
        {
            if (ModelState.IsValid)
            {
                bool result = repository.AddCourses(course);
                if (result)
                {
                    return Created("AddCourse", course);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/Admin/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Put(int id, [FromBody] Course course)
        {
            if (ModelState.IsValid)
            {
                bool result = repository.UpdateCourse(course);
                if (result)
                {
                    return Created("UpdatedCourse", course.Id);
                }
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var course = repository.GetCourse(id);
            if (course == null)
            {
                return NotFound();
            }
            bool result = repository.DeleteCourse(course);
            if (result)
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        [HttpGet("usersList")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsersList()
        {
            return Ok(repository.GetUsersList());
        }

        [HttpGet("mentorsList")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetMentorsList()
        {
            return Ok(repository.GetMentorsList());
        }
        [HttpGet("blockunblock/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetBlockUnblock(string id)
        {
            var result = repository.BlockUser(id);
            if (result)
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        // GET: api/Admin
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetEnrolledCourses()
        {
            return Ok(repository.GetEnrolledCourses());
        }

    }
}