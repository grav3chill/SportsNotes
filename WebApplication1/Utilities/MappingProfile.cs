using AutoMapper;
using SportsNotes.DTOs;
using SportsNotes.Entities;

namespace SportsNotes.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Workout, WorkoutDTO>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Exercise, ExerciseDTO>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ProgressRecord, ProgressRecordDTO>().ReverseMap().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
