 using System.Data;
 using Dapper;
using System.Linq;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using System.Dynamic;
using System.Text;

namespace DriverTask
{
	public class BaseRepository
	{
		protected readonly string _connectionString;
		public BaseRepository()
		{
			_connectionString = "Data Source=drivers.db";
		}
	}
}