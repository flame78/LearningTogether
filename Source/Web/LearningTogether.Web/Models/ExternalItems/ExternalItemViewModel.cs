namespace LearningTogether.Web.Models.ExternalItems
{
    using System.Linq;

    using AutoMapper;

    using LearningTogether.Data.Models;
    using LearningTogether.Web.Infrastructure.Extensions;
    using LearningTogether.Web.Infrastructure.Mapping;

    public class ExternalItemViewModel : IMapFrom<ExternalItem>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string ScreenShotName { get; set; }

        public RatingModel Rating { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ExternalItem, ExternalItemViewModel>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(y => new RatingModel { Id = y.Id, Rating = y.Ratings.Count != 0 ? (int)y.Ratings.Average(z => z.Value) : 0, Raters = y.Ratings.Count}));
        }
    }
}