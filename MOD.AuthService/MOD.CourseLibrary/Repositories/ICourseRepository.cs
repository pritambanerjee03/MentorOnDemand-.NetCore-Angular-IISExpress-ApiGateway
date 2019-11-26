using MOD.ModelLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOD.CourseLibrary.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetCourses();
        List<Course> SearchCourse(string criteria);
    }
}
