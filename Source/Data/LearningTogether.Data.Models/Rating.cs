using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTogether.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using LearningTogether.Data.Common.Models;

    public class Rating : BaseModel<int>
    {
        [Range(1, 5)]
        public int Value { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
