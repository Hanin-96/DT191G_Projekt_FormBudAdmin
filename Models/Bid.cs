namespace FormBudAdmin.Models
{
    public class Bid
    {
        //Properties
        public int Id { get; set; }
        public int Price { get; set; }

        //FK nycklar
        public int? BuyerId { get; set; }
        public int? ProductId { get; set; }



        //Navigation properties 
        //Buyer
        public Buyer? Buyer { get; set; }

        //Product
        public Product? Product { get; set; }

    }
}
