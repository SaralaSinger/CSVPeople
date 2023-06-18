using CSV.Data;
using CSV.Web.ViewModels;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;

namespace CSV.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleCSVController : ControllerBase
    {
        private readonly string _connectionString;
        public PeopleCSVController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpGet]
        [Route("GetAll")]
        public List<Person> GetAll()
        {
            var repo = new PersonRepo(_connectionString);
            return repo.GetPeople();
        }
        [HttpPost]
        [Route("Delete")]
        public void DeleteAll()
        {
            var repo = new PersonRepo(_connectionString);
            repo.DeletePeople();
        }
        [HttpPost]
        [Route("Upload")]
        public void Upload(ViewModel viewModel)
        {
            string base64 = viewModel.Base64.Substring(viewModel.Base64.IndexOf(",") + 1);
            byte[] fileBytes = Convert.FromBase64String(base64);
            var people = GetCsvFromBytes(fileBytes);
            var repo = new PersonRepo(_connectionString);
            repo.AddPeople(people);
        }
        [HttpGet]
        [Route("generate")]
        public IActionResult Generate(int amount)
        {
            var data = GenerateCSV(GeneratePeople(amount));
            return File(Encoding.UTF8.GetBytes(data), "text/csv", $"({amount}) people.csv");
        }
        
        private List<Person> GetCsvFromBytes(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            var streamReader = new StreamReader(memoryStream);
            using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            return reader.GetRecords<Person>().ToList();
        }

        private List<Person> GeneratePeople(int amount)
        {
            return Enumerable.Range(1, amount).Select(_ => new Person
            {
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                Age = Faker.RandomNumber.Next(20, 100),
                Address = Faker.Address.StreetAddress(),
                Email = Faker.Internet.Email()
            }).ToList();
        }
        private string GenerateCSV(List<Person> ppl)
        {
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);
            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(ppl);
            return builder.ToString();
        }
    }
}
