using MOD.ModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOD.CourseLibrary.Repositories
{
    public class CourseRepository : ICourseRepository
    {

        CourseContext context;
        public CourseRepository(CourseContext context)
        {
            this.context = context;
        }
        public IEnumerable<Course> GetCourses()
        {
            return this.context.Courses.ToList();
        }
        public List<Course> SearchCourse(string criteria)
        {
            if (int.TryParse(criteria, out int result))
            {
                return (from c in context.Courses
                        where c.Id == result
                        select c).ToList();
            }

            return (from c in context.Courses
                    where c.Name.Contains(criteria)
                    select c).ToList();

        }
    }
}
