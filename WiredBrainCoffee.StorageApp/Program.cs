using System;
using WiredBrainCoffee.StorageApp.Data;
using WiredBrainCoffee.StorageApp.Entities;
using WiredBrainCoffee.StorageApp.Repositories;

namespace WiredBrainCoffee.StorageApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var employeeRepository = new SqlRepository<Employee>(new StorageAppDbContext());
            employeeRepository.ItemAdded += EmployeeRepository_ItemAdded; ;
            AddEmployees(employeeRepository);
            AddManagers(employeeRepository);
            GetEmployeeById(employeeRepository);
            WriteAllToConsole(employeeRepository);


            var organizationRepository = new ListRepository<Organization>();
            AddOrginizations(organizationRepository);
            WriteAllToConsole(organizationRepository);

            Console.ReadLine();

        }

        private static void EmployeeRepository_ItemAdded(object? sender, Employee e)
        {
            Console.WriteLine($"Employee added => {e.FirstName}");
        }

        private static void AddManagers(IWriteRepository<Manager> managerRepository)
        {
            var matzManager = new Manager { FirstName = "Matz" };
            var matzManagerCopy = matzManager.Copy();
            managerRepository.Add(matzManager);

            if(matzManager is not null)
            {
                matzManagerCopy.FirstName += "_Copy";
                managerRepository.Add(matzManagerCopy);
            }

            managerRepository.Add(new Manager { FirstName = "Dylan" });
            managerRepository.Save();
        }

        private static void WriteAllToConsole(IReadRepository<IEntity> employeeRepository)
        {
            var items = employeeRepository.GetAll();
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        private static void GetEmployeeById(IRepository<Employee> employeeRepository)
        {
            var employee = employeeRepository.GetById(2);
            Console.WriteLine($"Employee with Id 2: {employee.FirstName}");
        }

        private static void AddEmployees(IRepository<Employee> employeeRepository)
        {
            var employees = new[]
            {
                new Employee { FirstName = "Hayden" },
                new Employee { FirstName = "Thomas" },
                new Employee { FirstName = "Skylar" }
        };
            employeeRepository.AddBatch(employees);

        }

        private static void AddOrginizations(IRepository<Organization> organizationRepository)
        {
            var organizations = new[]
            {new Organization { Id = 1, Name = "Pluralsight" },
             new Organization { Id = 2, Name = "Matz Farms" }
            };
            organizationRepository.AddBatch(organizations);
        }

       
    }
}
