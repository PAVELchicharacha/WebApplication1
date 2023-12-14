using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public DateTime SellDate { get; set; }
        [Required]
        public string Name { get; set; }
        public int SoldCount { get; set; }
        [Required]
        public int SoldPrice { get; set; }
    }
}
