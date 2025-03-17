using System.Collections.Generic;
 
namespace DriverTask
{
	public interface IDriverRepository
    {
        bool Save(Driver driver);
        IEnumerable<Driver> GetAll();
        Driver GetById(int id);
        bool Delete(int id);
    }
}