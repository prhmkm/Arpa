namespace ArpaBackend.Domain.DTOs
{
    public class StreamDTO
    {
        public class AddStreamRequest
        {
            public string Title { get; set; } = null!;
            public string Url { get; set; } = null!;
        }
        public class UpdateStreamRequest
        {
            public int Id { get; set; }
            public string Title { get; set; } = null!;
            public string Url { get; set; } = null!;
            public bool? IsActive { get; set; }
        }
    }
}
