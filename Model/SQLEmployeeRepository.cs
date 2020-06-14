using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext dbContext;

        public SQLEmployeeRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Employee Add(Employee Employee)
        {
            dbContext.Employees.Add(Employee); ;
            dbContext.SaveChanges();
            return Employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = dbContext.Employees.Find(id);
            if (employee != null) 
            {
                dbContext.Employees.Remove(employee);
                dbContext.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return dbContext.Employees;
        }

        public Employee GetEmployee(int Id)
        {
            return dbContext.Employees.Find(Id);
        }

        public Employee Update(Employee employeeChanges)
        {
            var employee = dbContext.Employees.Attach(employeeChanges);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dbContext.SaveChanges();
            return employeeChanges;
        }
    }
}
