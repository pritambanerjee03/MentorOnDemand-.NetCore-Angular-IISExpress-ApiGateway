using MOD.ModelLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MOD.DtoLibrary;

namespace MOD.StudentLibrary.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        StudentContext context;
        public StudentRepository(StudentContext context)
        {
            this.context = context;
        }
        public bool AddEnrolledCourses(EnrolledCourse enrolledCourse)
        {
            try
            {
                var result1 = from c in context.EnrolledCourses
                              where c.StudentEmail == enrolledCourse.StudentEmail
                                    && c.Name == enrolledCourse.Name
                              select c;
                if (result1.Count() == 0)
                {
                    context.EnrolledCourses.Add(enrolledCourse);
                    int result = context.SaveChanges();
                    if (result > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public bool ChangeCourseStatus(EnrolledCourse enrolledCourse, string UserEmail)
        {
            try
            {
                if (UserEmail == enrolledCourse.MentorEmail)
                {
                    if (enrolledCourse.Status == "Requested")
                    {
                        enrolledCourse.Status = "Request Accepted";
                    }
                    else if (enrolledCourse.Status == "In Progress")
                    {
                        enrolledCourse.Status = "Completed";
                    }
                    context.EnrolledCourses.Update(enrolledCourse);
                    int result = context.SaveChanges();
                    if (result > 0)
                    {
                        return true;
                    }
                }
                else if (UserEmail == enrolledCourse.StudentEmail && enrolledCourse.Status == "Request Accepted")
                {
                    enrolledCourse.Status = "In Progress";
                    context.EnrolledCourses.Update(enrolledCourse);
                    int result = context.SaveChanges();
                    if (result > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public List<EnrolledCourse> GetEnrolledCoursesByStudent(string modelEmail)
        {
            var result = from c in context.EnrolledCourses
                         where c.StudentEmail == modelEmail
                         select c;
            return result.ToList();
        }

        public UserDto studentProfileDetails(string email)
        {
            var result = from a in context.MODUsers
                         where a.Email == email
                         select new UserDto
                         {
                             id = a.Id,
                             FirstName = a.FirstName,
                             LastName = a.LastName,
                             Email = a.Email,
                             PhoneNumber = a.PhoneNumber
                         };
            return result.SingleOrDefault();
        }

        public bool UpdateStudentDetails(ProfileDto modUser, string studentId)
        {
            try
            {
                var user = (from a in context.MODUsers
                            where a.Id == studentId
                            select a).SingleOrDefault();
                if (user != null)
                {
                    user.Id = modUser.id;
                    user.Email = modUser.Email;
                    user.FirstName = modUser.FirstName;
                    user.LastName = modUser.LastName;
                    user.PhoneNumber = modUser.PhoneNumber;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
