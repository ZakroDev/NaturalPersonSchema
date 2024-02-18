using Microsoft.AspNetCore.Mvc;
using NaturalPerson.Core.Person;

namespace NaturalPerson.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ConnectionController(IPersonRepository personRepository) : Controller
    {

        [HttpPut]
        public async Task<IActionResult> Add([FromBody] Connection connection)
        {
            await personRepository.AddConnection(connection);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] int personId)
        {
            var result = await personRepository.GetConnections(personId);
            return Ok(new JsonResult(result));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int connectionId)
        {
            await personRepository.DeleteConnection(connectionId);
            return Ok();
        }
    }
}
