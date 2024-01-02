namespace BE_tasteal.Entity.DTO.Response
{
    public class CommentRes
    {
        public int id { get; set; }
        public string account_id { get; set; }
        public string name { get; set; }
        public string comment { get; set; }
        public string? image { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}
