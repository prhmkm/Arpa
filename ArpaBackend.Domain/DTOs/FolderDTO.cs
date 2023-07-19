using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArpaBackend.Domain.DTOs
{
    public class FolderDTO
    {
        public class GetFolderResponse
        {
            public List<Fldr> Folder { get; set; }
            public List<Pic> Picture { get; set; }
            public class Pic
            {
                public string Name { get; set; }
                public long PictureId { get; set; }
                public string Address { get; set; }
                public string ThumbAddress { get; set; }
            }
            public class Fldr
            {
                public string Title { get; set; }
                public long FolderId { get; set; }
            }
        }
        public class AddFolderRequest
        {
            public string Title { get; set; }
            public long ParentId { get; set; }
        }
        public class EditFolderRequest
        {
            public long ParentId { get; set; }
            public string Title { get; set; }
            public long Id { get; set; }
        }
        public class DeleteFolderRequest
        {
            public long Id { get; set; }
        }
        public class GetFolderRequest
        {
            public long FolderId { get; set; }
        }
    }
}
