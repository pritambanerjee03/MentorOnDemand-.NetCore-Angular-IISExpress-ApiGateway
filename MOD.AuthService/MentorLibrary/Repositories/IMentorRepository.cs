using MOD.DtoLibrary;
using MOD.ModelLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOD.MentorLibrary.Repositories
{
    public interface IMentorRepository
    {
        UserDto mentorProfileDetails(string email);
        List<EnrolledCourse> GetEnrolledCoursesByMentor(string modelEmail);
        bool ChangeCourseStatus(EnrolledCourse enrolledCourse, string UserEmail);
        bool UpdateMentorDetails(ProfileDto modUser, string mentorId);
    }
}
