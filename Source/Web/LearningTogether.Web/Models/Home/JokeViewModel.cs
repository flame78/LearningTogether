﻿namespace LearningTogether.Web.Models.Home
{
    using AutoMapper;

    using LearningTogether.Data.Models;
    using LearningTogether.Services.Web;
    using LearningTogether.Web.Infrastructure.Mapping;

    public class JokeViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public string Url
        {
            get
            {
                IIdentifierProvider identifier = new IdentifierProvider();
                return $"/Joke/{identifier.EncodeId(this.Id)}";
            }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            //todo:clear
            //configuration.CreateMap<Comment, JokeViewModel>()
            //    .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name));
        }
    }
}
