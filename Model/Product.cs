using EmployeeManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class Product : Common
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [Column(TypeName = "nvarchar(100)", Order = 2)]
        [DisplayName("Product")]
        public string ProductName { get; set; }

        [DisplayName("Quantity")]
        [Column(TypeName = "int", Order = 3)]
        public int Quantity { get; set; }

        [DisplayName("Price")]
        [Column(TypeName = "decimal(18,0)", Order = 4)]
        public double ProductPrice { get; set; }

        [DisplayName("Image")]
        [Column(TypeName = "nvarchar(500)", Order = 5)]
        public string ImagePath { get; set; }

        public virtual ICollection<BrandProduct> BrandProduct { get; set; }
    }
}

public class BrandProduct
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int BrandId { get; set; }
    public Brand Brands { get; set; }
    public int ProductId { get; set; }
    public Product Products { get; set; }
}