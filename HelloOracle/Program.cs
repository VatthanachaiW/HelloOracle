using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HelloOracle
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Firstname { get; set; }
        [DataType(DataType.EmailAddress)] public string Email { get; set; }
        [DataType(DataType.Date)] public DateTime SignUp { get; set; }
    }

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle("User Id = DemoUser; Password = Passw0rd; Data Source = 127.0.0.1:1521/ORCL");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new ApplicationDbContext())
            {
                // Insert new Record
                var employee = new Employee
                {
                    Firstname = "Vatthanachai",
                    Email = "vatthanachai.w@gmail.com",
                    SignUp = new DateTime(2011, 10, 14)
                };

                context.Employees.Add(employee);
                context.SaveChanges();
                

                // Update Exist Record
                var uemp = context.Employees.SingleOrDefault(s => s.Email == "vatthanachai.w@gmail.com");
                if (uemp != null)
                {
                    uemp.Firstname = "Vatthanachai Wongprasert";
                    uemp.SignUp = new DateTime(2015, 6, 11);
                    context.Update(uemp);
                    context.SaveChanges();
                }
                
                // Remove Exist Record
                var remp = context.Employees.SingleOrDefault(s => s.Email == "vatthanachai.w@gmail.com");
                if (remp != null)
                {
                    context.Remove(remp);
                    context.SaveChanges();
                }
            }

            Console.Read();
        }
    }
}