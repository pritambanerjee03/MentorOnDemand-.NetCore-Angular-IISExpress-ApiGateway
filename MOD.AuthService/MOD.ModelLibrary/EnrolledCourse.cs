﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MOD.ModelLibrary
{
    public class EnrolledCourse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string MentorEmail { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Duration { get; set; }

        [Required]
        [MaxLength(50)]
        public string CourseFee { get; set; }

        [Required]
        [MaxLength(50)]
        public string StudentEmail { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }


    }
}
