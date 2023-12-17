namespace BE_tasteal.Entity.DTO.Request
{
    public class PersonalCartItemReq
    {
        public int ingredient_id { get; set; }
        public string account_id { get; set; }
        public int amount { get; set; }
        public bool is_bought { get; set; }
    }
}
