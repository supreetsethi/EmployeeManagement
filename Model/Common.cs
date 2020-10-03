using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Model
{
    public class Common
    {
        [DisplayName("Is Active")]
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string CreatedBy { get; set; }

        private DateTime _createdDate;

        [Column(TypeName = "DateTime2")]
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = DateTime.Now; }
        }


        [Column(TypeName = "DateTime2")]
        public DateTime UpdatedDate
        {
            get { return _createdDate; }
            set { _createdDate = DateTime.Now; }
        }

        [Column(TypeName = "nvarchar(100)")]
        public string UpdatedBy { get; set; }
    }
}
