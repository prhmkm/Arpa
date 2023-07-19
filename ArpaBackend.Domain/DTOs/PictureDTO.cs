using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArpaBackend.Domain.DTOs
{
    public class PictureDTO
    {
        public class UploadRequest
        {
            public long FolderId { get; set; }
            public string PictureName { get; set; }
            public string Picture { get; set; }
            public bool Thumbnail { get; set; }
        }
        public class DeleteRequest
        {
            public long Id { get; set; }
        }
        public class UploadPic
        {
            public long Id { get; set; }
            public string Address { get; set; }
        }
        public class PictureResponse
        {
            public long Id { get; set; }
            public string ImageName { get; set; }
            public string Address { get; set; }
            public string Thumbnail { get; set; }
            public long FolderId { get; set; }
        }
    }
}
