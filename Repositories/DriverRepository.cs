using System.Collections.Generic;
using System.Linq;
using CodeKata.Models;
using CodeKata.Utils;

namespace CodeKata.Repositories
{
    public class DriverRepository : IRepository<Driver>
    {
        private readonly List<Driver> drivers;

        public DriverRepository()
        {
            drivers = new List<Driver>();
        }

        public Driver GetByName(string name)
        {
            return drivers.FirstOrDefault(drv => drv.Name == name);

        }

        public void Add(Driver driver)
        {
            Guard.IsNotNull(driver);

            if (GetByName(driver.Name) == null)
            {
                drivers.Add(driver);
            }
        }

        public List<Driver> GetAll()
        {
            return drivers;
        }
    }
}
