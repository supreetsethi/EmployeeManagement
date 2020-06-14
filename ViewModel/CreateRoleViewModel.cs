using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModel
{
    
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }

}

