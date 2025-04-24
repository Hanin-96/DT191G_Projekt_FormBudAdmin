using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormBudAdmin.Models
{
    public class Product
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
        //Filuppladdning
        public string? ImageName { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
