﻿using System.Collections.Generic;

namespace SimpleDataService.BAL
{
    public class CourseVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? SubjectId { get; set; }
        public int? SpecializationId { get; set; }
        public int? AmountOfHours { get; set; }
        public ICollection<SubjectVM> Subjects { get; set; }
        public string Specialization { get; set; }
    }
}
