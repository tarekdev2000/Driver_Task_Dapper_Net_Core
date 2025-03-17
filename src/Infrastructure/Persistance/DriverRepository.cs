 using System.Data;
 using Dapper;
using System.Linq;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using System.Dynamic;
using System.Text;
using Microsoft.Data.Sqlite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DriverTask
{
	public class DriverRepository : BaseRepository, IDriverRepository
    {
		private IDbConnection _connection;

		public DriverRepository()
		{
			_connection = new SqliteConnection(_connectionString);
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());

            _connection.Open();
		}

		

		/// <summary>
		/// Inserts or updates a driver record.
		/// </summary>
		/// <param name="author"></param>
		/// <returns></returns>
		public bool Save(Driver driver)
		{
			if(driver.Id == 0)
			{
                driver.Id = (int)_connection.Insert<Driver>(driver);
			}
			else
			{
				_connection.Update<Driver>(driver);
			}
			return true;
		}

		/// <summary>
		/// Get All Drivers.
		/// </summary>
		/// <param name="author"></param>
		/// <returns></returns>
		public IEnumerable<Driver> GetAll()
		{
			var driverList = _connection.Query<Driver>("SELECT * FROM Drivers");
			return driverList;
		}

		/// <summary>
		/// Get Driver By Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public Driver GetById(int id)
		{
			var driver = _connection.QueryFirstOrDefault<Driver>("SELECT * FROM Drivers WHERE Id = @Id", new { Id = id });
			return driver;

        }

		/// <summary>
		/// Delete Driver by Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public bool Delete(int id)
		{
            var sql = "DELETE FROM Drivers WHERE Id = @Id";
            var deleted = _connection.Execute(sql, new { Id = id });

			return deleted > 0;
        }

		public List<Driver> PopulateRandomTenRecords()
		{
			List<Driver> driverList = new List<Driver>();
            for (int i = 0; i < 10; i++)
            {
				Driver driver = new Driver()
				{
					Email = $"email{i}@test.com",
					FirstName = $"First{i}",
					LastName = $"Last{i}",
					PhoneNumber = $"123-456-789{i}"
                
				};
				//insert record
				Save(driver);
				driverList.Add(driver);
             }
			return driverList;

        }

    }
}