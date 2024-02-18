namespace NaturalPerson.Core.Person
{
    public interface IPersonRepository
    {
        Task Add(Person person);
        Task AddConnection(Connection connection);
        Task DeleteConnection(int connectionId);
        Task DeletePerson(int personId);
        Task<List<Connection>> GetConnections(int personId);
        Task<Person> GetPerson(int id);
        Task<List<Person>> SearchPerson(string keyword, int numberOfObjectsPerPage, int pageNumber);
        Task Update(Person person);
        Task AddPicture(string pictureFullPath, int personId);
    }
}