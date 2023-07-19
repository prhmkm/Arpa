using System;
using System.Collections.Generic;

namespace ArpaBackend.Domain.Models
{
    public partial class Festival
    {
        public Festival()
        {
            FestivalVideos = new HashSet<FestivalVideo>();
        }

        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CoverURL { get; set; } = null!;
        public DateTime CreationDateTime { get; set; }
        public bool IsDelete { get; set; }
        public int VideoSize { get; set; }
        public int VideoLength { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<FestivalVideo> FestivalVideos { get; set; }
    }
}
