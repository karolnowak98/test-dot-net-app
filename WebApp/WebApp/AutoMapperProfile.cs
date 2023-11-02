namespace WebApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterResponseDto>();
            CreateMap<AddCharacterRequestDto, Character>();
            CreateMap<UpdateCharacterRequestDto, Character>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.HitPoints, opt => opt.MapFrom(src => src.HitPoints))
                .ForMember(dest => dest.Strength, opt => opt.MapFrom(src => src.Strength))
                .ForMember(dest => dest.Defense, opt => opt.MapFrom(src => src.Defense))
                .ForMember(dest => dest.Intelligence, opt => opt.MapFrom(src => src.Intelligence))
                .ForMember(dest => dest.Class, opt => opt.MapFrom(src => src.Class));
            CreateMap<Character, UpdateCharacterRequestDto>();
        }
    }
}