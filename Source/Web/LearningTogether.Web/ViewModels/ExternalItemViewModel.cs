namespace LearningTogether.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    using AutoMapper;

    using LearningTogether.Common;
    using LearningTogether.Data.Models;
    using LearningTogether.Web.Infrastructure.Mapping;

    public class ExternalItemViewModel : IMapFrom<ExternalItem>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string ScreenShotName { get; set; }

        public double? Rating { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<ExternalItem, ExternalItemViewModel>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(y => y.Ratings.Average(z => z.Value)));
        }
    }
}