using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int Id);

        IEnumerable<Employee> GetAllEmployee();

        Employee Add(Employee Employee);

        Employee Update(Employee employeeChanges);

        Employee Delete(int id);
    }
}
