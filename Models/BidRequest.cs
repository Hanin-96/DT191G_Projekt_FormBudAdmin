namespace FormBudAdmin.Models
{
    public class BidRequest
    {
        //Properties
        public string? BuyerName { get; set; }
        public string? BuyerEmail { get; set; }
        public string? BuyerPhone { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
    }
}
