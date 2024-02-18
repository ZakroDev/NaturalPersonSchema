using Microsoft.AspNetCore.Mvc;
using NaturalPerson.Core.Person;
using NaturalPersonl.Infra.Person.Exceptions;
using System.Transactions;

namespace NaturalPerson.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PersonController(IPersonRepository personRepository, IFileService fileService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Get(int personId)
        {
            Person result = await personRepository.GetPerson(personId);
            return Ok(new JsonResult(result));
        }
        [HttpPut]
        public async Task<IActionResult> Add([FromBody] Person person)
        {
            await personRepository.Add(person);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] Person person)
        {
            await personRepository.Update(person);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int personId)
        {
            await personRepository.DeletePerson(personId);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> SearchPersons(string keyword, int numberOfObjectsPerPage, int pageNumber)
        {
            List<Person> people = await personRepository.SearchPerson(keyword, numberOfObjectsPerPage, pageNumber);

            return Ok(new JsonResult(people));
        }

        [HttpPut]
        public async Task<IActionResult> UploadPicture(int personId, IFormFile file)
        {
            string pictureUrl = await fileService.UploadAsync(file);
            await personRepository.AddPicture( pictureUrl, personId);

            return Ok();
        }
    }
}
