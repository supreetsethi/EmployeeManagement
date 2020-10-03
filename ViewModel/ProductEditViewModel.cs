using System;

namespace EmployeeManagement.ViewModel
{
    public class ProductEditViewModel : ProductCreateViewModel
    {
        public int Id { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
