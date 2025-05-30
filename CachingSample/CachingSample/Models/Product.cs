using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CachingSample.Models
{
    public class Product
    {
        [Key]
        [Column("id")]  // <-- map rõ tên cột
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = default;

        [Column("description")]
        public string Description { get; set; } = default;

        [Column("price")]
        public decimal Price { get; set; }

        private Product() { }

        public Product(string name, string description, decimal price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
        }
    }
}
