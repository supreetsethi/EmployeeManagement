using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee(){  Id = 1, Name = "Supreet",   Department= Dept.IT,   Email="Supreet1986@gmail.com" },
                new Employee() { Id = 2, Name = "Gaurav", Department = Dept.HR, Email = "Gaurav@gmail.com" },
                new Employee() { Id = 3, Name = "Mohit", Department = Dept.Payroll, Email = "Mohit@gmail.com" }
            };

        }

        public Employee Add(Employee Employee)
        {
            Employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(Employee);
            return Employee;
        }

        public Employee Delete(int id)
        {
            var employee = _employeeList.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(x => x.Id == Id);
        }

        public Employee Update(Employee employeeChanges)
        {
            var employee = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }
            return employee;
        }
    }
}
