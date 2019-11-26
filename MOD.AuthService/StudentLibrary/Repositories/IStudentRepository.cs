using MOD.DtoLibrary;
using MOD.ModelLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOD.StudentLibrary.Repositories
{
    public interface IStudentRepository
    {
        bool UpdateStudentDetails(ProfileDto studentData, string studentId);
        UserDto studentProfileDetails(string email);
        List<EnrolledCourse> GetEnrolledCoursesByStudent(string modelEmail);
        bool AddEnrolledCourses(EnrolledCourse enrolledCourse);
        bool ChangeCourseStatus(EnrolledCourse enrolledCourse, string UserEmail);
    }
}
