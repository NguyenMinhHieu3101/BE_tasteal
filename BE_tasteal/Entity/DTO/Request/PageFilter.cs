namespace BE_tasteal.Entity.DTO.Request
{
    public class PageFilter
    {
        public int pageSize { get; set; }
        public int page { get; set; }
        public bool isDescend { get; set; }
    }
}
