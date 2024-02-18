using Microsoft.EntityFrameworkCore;
using NaturalPerson.Core.Person;
using NaturalPerson.Infra.Db;
using NaturalPersonl.Infra.Person.Exceptions;
using System.Linq;

namespace NaturalPerson.Infra.Person
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonContext context;
        public PersonRepository(PersonContext context)
        {
            this.context = context;
        }
        public async Task Add(Core.Person.Person person)
        {
            try
            {
                Model.Person modelPerson = ToInfraPerson(person);

                await context.AddAsync(modelPerson);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new PersonAddException(ex.Message);
            }
        }

        public async Task Update(Core.Person.Person person)
        {
            try
            {
                Model.Person modelPerson = await context.Persons.FirstAsync(x => x.Id == person.Id);
                modelPerson.FirstName = person.FirstName;
                modelPerson.LastName = person.LastName;
                modelPerson.PersonalId = person.PersonalId;
                modelPerson.Gender = (Model.PersonGender)person.Gender;
                modelPerson.BirthDate = person.BirthDate;

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new PersonUpdateException(ex.Message);
            }
        }
        public async Task AddPicture(string pictureFullPath, int personId)
        {
            try
            {
                Model.Person person = await context.Persons.FirstAsync(x => x.Id == personId);
                person.Photo = pictureFullPath;

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new AddPictureException(ex.Message);
            }
        }

        public async Task DeletePerson(int personId)
        {
            try
            {
                Model.Person person = await context.Persons.FirstAsync(x => x.Equals(personId));
                context.Persons.Attach(person);
                context.Remove(person);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DeletePersonException(ex.Message);
            }
        }

        public async Task AddConnection(Connection connection)
        {
            try
            {
                Model.Connection personConnection = new()
                {
                    PersonId = connection.PersonId,
                    ConnectedPersonId = connection.ConnectedPersonId,
                    ConnectionType = (Model.ConnectedPersonType)connection.ConnectionType
                };

                context.Add(personConnection);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new AddConnectionException(ex.Message);
            }
        }

        public async Task<List<Core.Person.Connection>> GetConnections(int personId)
        {
            List<Model.Connection> connection = await context.Connections.Where(x => x.Id == personId).ToListAsync();

            List<Core.Person.Connection> result = connection.Select(x => new Connection
            {
                ConnectionType = (ConnectedPersonType)x.ConnectionType,
                PersonId = x.PersonId,
                ConnectedPersonId = x.ConnectedPersonId
            }).ToList();


            return result;
        }

        public async Task DeleteConnection(int connectionId)
        {
            try
            {
                Model.Connection? connection = context.Connections.FirstOrDefault(x => x.Id == connectionId);
                context.Connections.Attach(connection);
                context.Remove(connection);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DeleteConnectionException(ex.Message);
            }
        }

        public async Task<Core.Person.Person> GetPerson(int id)
        {
            Model.Person person = await context.Persons
                                    .Include(x => x.City)
                                    .Include(x => x.Phone)
                                    .Include(x => x.Connection)
                                    .FirstAsync(x => x.Id == id);

            Core.Person.Person result = ToDomainPerson(person);
            return result;
        }

        public async Task<List<Core.Person.Person>> SearchPerson(string keyword, int numberOfObjectsPerPage, int pageNumber)
        {
            List<Model.Person> people = await context.Persons
                           .Include(x => x.City)
                           .Include(x => x.Phone)
                           .Include(x => x.Connection)
                           .Where(x => x.FirstName.Contains(keyword) ||
                           x.LastName.Contains(keyword) ||
                           x.PersonalId.Contains(keyword))
                           .Skip(numberOfObjectsPerPage * pageNumber)
                           .Take(numberOfObjectsPerPage)
                           .ToListAsync();

            List<Core.Person.Person> result = people.Select(x => ToDomainPerson(x)).ToList();

            return result;
        }

        private static Core.Person.Person ToDomainPerson(Model.Person person)
        {
            return new()
            {
                Id = person.Id,
                PersonalId = person.PersonalId,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = (PersonGender)person.Gender,
                BirthDate = person.BirthDate,
                City = new PersonsCity() { Id = person.City?.Id, Name = person.City?.Name },
                Photo = person.Photo,
                Phone = person.Phone?.Select(x => new PhoneNumber { Phone = x.Phone, Type = (PhoneType)x.Type }).ToList(),
                ConnectedPerson = person.Connection?
                                        .Select(x =>
                                        new Connection { PersonId = x.PersonId, ConnectionType = (ConnectedPersonType)x.ConnectionType })
                                        .ToList()
            };
        }

        private static Model.Person ToInfraPerson(Core.Person.Person person)
        {
            Model.Person modelPerson = new Model.Person
            {
                PersonalId = person.PersonalId,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = (Model.PersonGender)person.Gender,
                CityId = person?.City?.Id
            };
            return modelPerson;
        }
    }
}
