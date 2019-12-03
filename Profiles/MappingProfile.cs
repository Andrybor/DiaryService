using AutoMapper;
using SimpleDataService.BAL;
using SimpleDataService.DAL;

namespace SimpleDataService.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserVM>();

            CreateMap<UserVM, User>();

            CreateMap<Account, AccountVM>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType));

            CreateMap<AccountVM, Account>(); 

            CreateMap<AccountType, AccountTypeVM>();

            CreateMap<Specialization, SpecializationVM>()
                .ForMember(dest=>dest.Courses,opt=>opt.MapFrom(src=>src.Course))
                .ForMember(dest=>dest.Groups,opt=>opt.MapFrom(src=>src.Group));

            CreateMap<Course, CourseVM>()
                .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.Subject))
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialization.Title));

            CreateMap<Subject, SubjectVM>()
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course.Title));

            CreateMap<Auditorium, AuditoriumVM>();

            CreateMap<Group, GroupVM>()
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Student));

            CreateMap<Schedule, ScheduleVM>()
                .ForMember(dest => dest.Auditorium, opt => opt.MapFrom(src => src.Auditorium))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group))
                .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => src.Teacher));

            CreateMap<TeacherSkill, TeacherVM>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Teacher))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject));

            CreateMap<News, NewsVM>();

            CreateMap<Student, StudentVM>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group.Title));

            CreateMap<Homework, HomeWorkVM>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Schedule.StartTime))
                .ForMember(dest => dest.Theme, opt => opt.MapFrom(src => src.Schedule.Theme));

            CreateMap<LessonInfo, LessonInfoVM>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => src.Schedule));

        }
    }
}
