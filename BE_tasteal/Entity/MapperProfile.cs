using AutoMapper;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using System.Text.RegularExpressions;

namespace BE_tasteal.Entity
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region Request DTO to Entity
            CreateMap<RecipeDto, RecipeEntity>()
                .ForMember(destination => destination.id, destination => destination.Ignore())
                .ForMember(source => source.name, destination => destination.MapFrom(src => src.name))
                .ForMember(source => source.rating, destination => destination.MapFrom(src => src.rating))
                .ForMember(source => source.totalTime, destination => destination.MapFrom(src => ParseTimeSpan(src.totalTime)))
                .ForMember(source => source.active_time, destination => destination.MapFrom(src => ParseTimeSpan(src.active_time)))
                .ForMember(source => source.serving_size, destination => destination.MapFrom(src => src.serving_size))
                .ForMember(source => source.introduction, destination => destination.MapFrom(src => src.introduction))
                .ForMember(source => source.author_note, destination => destination.MapFrom(src => src.author_note))
                .ForMember(source => source.is_private, destination => destination.MapFrom(src => src.is_private))
                .ForMember(source => source.image, destination => destination.MapFrom(src => src.image))
                .ForMember(source => source.author, destination => destination.MapFrom(src => src.author))
                .ForMember(destination => destination.ingredients, destination => destination.Ignore())
                .ForMember(destination => destination.direction, destination => destination.Ignore());

            // CreateMap<Recipe_DirectionEntity, Recipe_DirectionEntity>();
            #endregion
        }

        static TimeSpan ParseTimeSpan(string input)
        {
            Regex regex = new Regex(@"(?:(\d+)h)?(?:(\d+)m)?(?:(\d+)s)?");
            Match match = regex.Match(input);

            int hours = match.Groups[1].Success ? int.Parse(match.Groups[1].Value) : 0;
            int minutes = match.Groups[2].Success ? int.Parse(match.Groups[2].Value) : 0;
            int seconds = match.Groups[3].Success ? int.Parse(match.Groups[3].Value) : 0;

            TimeSpan timeSpan = new TimeSpan(hours, minutes, seconds);
            return timeSpan;
        }
    }
}
