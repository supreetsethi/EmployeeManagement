using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee()
                {
                    Id = 1,
                    Name = "Supreet",
                    Email = "Supreet1986@gmail.com",
                    Department = Dept.HR
                },
                new Employee()
                {
                    Id = 2,
                    Name = "Sakshi",
                    Email = "Sakshi@gmail.com",
                    Department = Dept.IT
                }
            );
        }
    }
}
