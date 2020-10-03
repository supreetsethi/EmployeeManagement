using EmployeeManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class Brand : Common
    {
        public int BrandId { get; set; }
        [DisplayName("Brand")]
        public string BrandName { get; set; }
        public virtual ICollection<BrandCategory> BrandCategory { get; set; }
        public virtual ICollection<BrandProduct> BrandProduct { get; set; }
    }
}
