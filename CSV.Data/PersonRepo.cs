namespace CSV.Data
{
    public class PersonRepo
    {
        private readonly string _connectionString;
        public PersonRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddPeople(List<Person> people)
        {
            var context = new DataContext(_connectionString);
            context.AddRange(people);
            context.SaveChanges();
        }
        public List<Person> GetPeople()
        {
            var context = new DataContext(_connectionString);
            return context.People.ToList();
        }
        public void DeletePeople()
        {
            var context = new DataContext(_connectionString);
            context.People.RemoveRange(context.People.ToList());
            context.SaveChanges();
        }
    }
}