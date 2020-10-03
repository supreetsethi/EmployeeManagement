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
    public class Category : Common
    {
        public int CategoryId { get; set; }
        
        [DisplayName("Category")]
        public string Categories { get; set; }

        public virtual ICollection<BrandCategory> BrandCategory { get; set; }
    }
}


public class BrandCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int BrandId { get; set; }
    public Brand Brands { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}