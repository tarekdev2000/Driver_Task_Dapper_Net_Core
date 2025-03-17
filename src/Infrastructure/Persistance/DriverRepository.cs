 using System.Data;
 using Dapper;
using System.Linq;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using System.Dynamic;
using System.Text;
using Microsoft.Data.Sqlite;

namespace DriverTask
{
	public class DriverRepository : BaseRepository, IDriverRepository
    {
		private IDbConnection _connection;

		public DriverRepository()
		{
			_connection = new SqliteConnection(_connectionString);
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

    }
}