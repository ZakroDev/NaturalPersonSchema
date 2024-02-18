namespace NaturalPerson.Infra.Model
{
    public class City
    {
        public required int Id { get; set; }
        public required string Name { get; set; }

        public List<Person>? People { get; set; }
    }
}
