using MOD.DtoLibrary;
using MOD.ModelLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MOD.MentorLibrary.Repositories
{
    public class MentorRepository : IMentorRepository
    {
        MentorContext context;
        public MentorRepository(MentorContext context)
        {
            this.context = context;
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

        public List<EnrolledCourse> GetEnrolledCoursesByMentor(string modelEmail)
        {
            var result = from c in context.EnrolledCourses
                         where c.MentorEmail == modelEmail
                         select c;
            return result.ToList();
        }

        public UserDto mentorProfileDetails(string email)
        {
            var result = from a in context.MODUsers
                         where a.Email == email
                         select new UserDto
                         {
                             id = a.Id,
                             Experience = a.Experience,
                             FirstName = a.FirstName,
                             LastName = a.LastName,
                             Skills = a.Skills,
                             Email = a.Email,
                             PhoneNumber = a.PhoneNumber
                         };
            return result.SingleOrDefault();
        }

        public bool UpdateMentorDetails(ProfileDto modUser, string mentorId)
        {
            try
            {
                var user = (from a in context.MODUsers
                            where a.Id == mentorId
                            select a).SingleOrDefault();
                if (user != null)
                {
                    user.Id = modUser.id;
                    user.Email = modUser.Email;
                    user.FirstName = modUser.FirstName;
                    user.LastName = modUser.LastName;
                    user.PhoneNumber = modUser.PhoneNumber;
                    user.Skills = modUser.Skills;
                    user.Experience = modUser.Experience;

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
