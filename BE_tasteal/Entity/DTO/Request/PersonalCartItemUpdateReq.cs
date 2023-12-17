namespace BE_tasteal.Entity.DTO.Request
{
    public class PersonalCartItemUpdateReq
    {
        public int id {  get; set; }
        public string name { get; set; }
        public int amount { get; set; }
        public bool is_bought { get; set; }
    }
}
