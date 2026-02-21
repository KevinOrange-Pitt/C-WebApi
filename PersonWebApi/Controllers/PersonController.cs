using Microsoft.AspNetCore.Mvc;
using PersonWebApi.Models;
using PersonWebApi.Services;

namespace PersonWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly FileManager _fileManager;

        public PersonController()
        {
            _fileManager = FileManager.Instance;
        }

        /// <summary>
        /// Get all persons
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetAll()
        {
            var persons = _fileManager.GetAllPersons();
            return Ok(persons);
        }

        /// <summary>
        /// Get a person by ID
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Person> GetById(string id)
        {
            var person = _fileManager.GetPersonById(id);
            
            if (person == null)
            {
                return NotFound(new { message = $"Person with ID '{id}' not found" });
            }
            
            return Ok(person);
        }

        /// <summary>
        /// Create a new person
        /// </summary>
        [HttpPost]
        public ActionResult<Person> Create([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest(new { message = "Person data is required" });
            }

            if (string.IsNullOrWhiteSpace(person.Name))
            {
                return BadRequest(new { message = "Name is required" });
            }

            if (string.IsNullOrWhiteSpace(person.School))
            {
                return BadRequest(new { message = "School is required" });
            }

            var createdPerson = _fileManager.AddPerson(person);
            return CreatedAtAction(nameof(GetById), new { id = createdPerson.Id }, createdPerson);
        }

        /// <summary>
        /// Update an existing person
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult<Person> Update(string id, [FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest(new { message = "Person data is required" });
            }

            if (string.IsNullOrWhiteSpace(person.Name))
            {
                return BadRequest(new { message = "Name is required" });
            }

            if (string.IsNullOrWhiteSpace(person.School))
            {
                return BadRequest(new { message = "School is required" });
            }

            var success = _fileManager.UpdatePerson(id, person);
            
            if (!success)
            {
                return NotFound(new { message = $"Person with ID '{id}' not found" });
            }

            var updatedPerson = _fileManager.GetPersonById(id);
            return Ok(updatedPerson);
        }

        /// <summary>
        /// Delete a person
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var success = _fileManager.DeletePerson(id);
            
            if (!success)
            {
                return NotFound(new { message = $"Person with ID '{id}' not found" });
            }

            return Ok(new { message = "Person deleted successfully" });
        }
    }
}
