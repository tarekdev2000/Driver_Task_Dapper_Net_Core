using DriverTask;
using DriverTask.Api.Controllers.v1;
 using DriverTask.ApiFramework.Tools;
using DriverTask.Common.Utilities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Api.Controllers.v1
{
    [ApiVersion("1")]
    public class DriverController : BaseControllerV1
    {
        private readonly IDriverRepository _driverRepository;
        public DriverController(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }


        [HttpPost]
        [SwaggerOperation("add a driver")]
        public async Task<IActionResult> Create([FromBody] Driver driver)
        {
            if (driver == null)
            {
                throw new System.Exception("you can not supply null object");
            }
            if (!IsValidEmail(driver.Email))
            {
                throw new System.Exception("you must supply valid email");
            }
            if (!IsValidPhoneNumber(driver.Email))
            {
                throw new System.Exception("you must supply valid phone number");
            }

            var result = _driverRepository.Save(driver);
            return Created($"api/drivers/{driver.Email}", driver);

        }

        [HttpPut("{id}")]
        [SwaggerOperation("update a driver")]
        public IActionResult Update(int id, Driver driver)
        {
            var updated = _driverRepository.Save(driver);
            return updated ? NoContent() : NotFound();
        }

        [HttpGet("{id}")]
        [SwaggerOperation("get a driver by id")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] int id)
        {
            var driver = _driverRepository.GetById(id);
            return driver is null ? NotFound() : Ok(driver);
        }

        [HttpGet]
        [SwaggerOperation("get a driver User Name Alphabetized by id")]
        [Route("Alphabetized")]

        public async Task<IActionResult> GetByIdAlphabetized([FromQuery] int id)
        {
            var driver = _driverRepository.GetById(id);

            string alphabetized = new string((driver.FirstName + " " + driver.LastName).OrderBy(c => c).ToArray());

            return driver is null ? NotFound() : Ok(alphabetized);
        }

        [HttpGet("all")]
        [SwaggerOperation("get all drivers")]
        public async Task<IActionResult> GetAllAsync()
        {

            var drivers = _driverRepository.GetAll();
            return Ok(drivers);
        }

        [HttpGet("Populate")]
        [SwaggerOperation("Add new 10 random drivers")]
        public async Task<IActionResult> PopulateRandomTenRecords()
        {

            List<Driver> drivers = _driverRepository.PopulateRandomTenRecords();
            return Ok(drivers);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _driverRepository.Delete(id);
            return deleted ? NoContent() : NotFound();
        }


        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // Regex pattern for common phone number formats (supports international and US-style numbers)
            string pattern = @"^\+?[1-9]\d{0,2}[-.\s]?\(?\d{1,4}\)?[-.\s]?\d{1,4}[-.\s]?\d{1,9}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}