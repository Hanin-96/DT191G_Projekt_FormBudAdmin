namespace FormBudAdmin.ViewModels
{
    public class CreateProductViewModel
    {
        //Properties
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int Worth { get; set; }
        public int MinPrice { get; set; }
        public string? Condition { get; set; }
        public DateTime TimeLeft { get; set; }
        public bool IsSold { get; set; } = false;
        public IFormFile Image { get; set; }
    }
}
