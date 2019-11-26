using MOD.DtoLibrary;
using MOD.ModelLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOD.AdminLibrary.Repositories
{
    public interface IAdminRepository
    {
        bool AddCourses(Course course);
        bool UpdateCourse(Course course);
        Course GetCourse(int id);
        bool DeleteCourse(Course course);
        IEnumerable<UserDto> GetMentorsList();
        IEnumerable<UserDto> GetUsersList();
        bool BlockUser(string id);
        IEnumerable<EnrolledCourse> GetEnrolledCourses();
    }
}
