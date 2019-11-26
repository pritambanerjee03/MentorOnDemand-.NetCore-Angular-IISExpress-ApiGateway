using MOD.DtoLibrary;
using MOD.ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOD.AdminLibrary.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        AdminContext context;
        public AdminRepository(AdminContext context)
        {
            this.context = context;
        }

        public bool AddCourses(Course course)
        {
            try
            {
                context.Courses.Add(course);
                int result = context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public bool BlockUser(string id)
        {
            {
                var userblock = context.MODUsers.SingleOrDefault(u => u.Id == id);
                userblock.Active = !userblock.Active;
            }
            var result = context.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteCourse(Course course)
        {
            try
            {
                context.Courses.Remove(course);
                int result = context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Course GetCourse(int id)
        {
            var result = from a in context.Courses
                         where a.Id == id
                         select a;
            return result.SingleOrDefault();
        }

        public IEnumerable<EnrolledCourse> GetEnrolledCourses()
        {

            return this.context.EnrolledCourses.ToList();
        }

        public IEnumerable<UserDto> GetMentorsList()
        {
            var mentor = from a in context.MODUsers
                         join ma in context.UserRoles on a.Id equals ma.UserId
                         where ma.RoleId == "2"
                         select new UserDto
                         {
                             id = a.Id,
                             Active = a.Active,
                             Experience = a.Experience,
                             FirstName = a.FirstName,
                             LastName = a.LastName,
                             Skills = a.Skills,
                             Email = a.Email,
                             PhoneNumber = a.PhoneNumber

                         };
            return mentor;
        }

        public IEnumerable<UserDto> GetUsersList()
        {
            var user = from a in context.MODUsers
                       join ma in context.UserRoles on a.Id equals ma.UserId
                       where ma.RoleId == "3"
                       select new UserDto
                       {
                           id = a.Id,
                           Active = a.Active,
                           FirstName = a.FirstName,
                           LastName = a.LastName,
                           Email = a.Email,
                           PhoneNumber = a.PhoneNumber

                       };
            return user;
        }

        public bool UpdateCourse(Course course)
        {
            try
            {
                context.Courses.Update(course);
                int result = context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
