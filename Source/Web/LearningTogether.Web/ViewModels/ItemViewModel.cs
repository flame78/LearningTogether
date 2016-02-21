namespace LearningTogether.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;


    using LearningTogether.Common;
    using LearningTogether.Data.Models;
    using LearningTogether.Web.Infrastructure.Mapping;

    public class ItemViewModel : IMapFrom<ExternalItem>
    {
        public int Id { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string ScreenShotName { get; set; }

        public string EncodedLink => Convert.ToBase64String(Encoding.UTF8.GetBytes(this.Link));
    }
}