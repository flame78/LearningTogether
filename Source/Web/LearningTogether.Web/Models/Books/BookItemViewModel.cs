namespace LearningTogether.Web.Models.Books
{
    using System.Linq;

    using AutoMapper;

    using LearningTogether.Data.Models;
    using LearningTogether.Web.Infrastructure.Extensions;
    using LearningTogether.Web.Infrastructure.Mapping;

    public class BookItemViewModel : IMapFrom<BookItem>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }

        public string ISBN { get; set; }

        public string BookAuthor { get; set; }

        public string Title { get; set; }

        public RatingModel Rating { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<BookItem, BookItemViewModel>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(y => new RatingModel { Id = y.Id, Rating = y.Ratings.Count != 0 ? (int)y.Ratings.Average(z => z.Value) : 0, Raters = y.Ratings.Count }))
                .ForMember(x => x.Category, opt => opt.MapFrom(y => y.Category.Name));

        }
    }
}