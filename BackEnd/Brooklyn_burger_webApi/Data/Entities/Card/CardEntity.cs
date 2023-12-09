namespace Internet_Market_WebApi.Data.Entities.Card
{
    public class CardEntity: BaseEntity<int>//NOT IN USE
    {
        public string CardNumber { get; set; }
        public DateTime ExpireDate { get; set; }
        public int CVV { get; set; }    
    }
}
