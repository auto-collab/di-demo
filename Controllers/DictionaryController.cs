using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;

namespace demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DictionaryController : Controller
    {
        private readonly IDictionaryService _dictionaryService;

        public DictionaryController(IDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        [HttpGet("{word}")]
        public IActionResult GetDefinition(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return BadRequest("Please enter a valid word.");
            }

            if (word != null)
            {
                var definition = _dictionaryService.GetDefintion(word);
                return Ok(definition);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddWord([FromBody] WordEntry entry)
        {
            if (string.IsNullOrEmpty(entry.word) || string.IsNullOrEmpty(entry.definition))
            {
                return BadRequest("Please enter a valid word and definition.");
            }

            if (_dictionaryService.AddWord(entry.word, entry.definition))
            {

                return Ok($"{entry.word} succesfully added to dictionary");
            }
            else
            {
                return Conflict($"{entry.word} already exists in dictionary.");
            }

        }


        [HttpDelete("{word}")]
        public IActionResult DeleteWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return BadRequest("Please enter a valid word.");
            }

            if (_dictionaryService.RemoveWord(word))
            {
                return Ok($"{word} removed from dictionary.");
            }
            else
            {
                return NotFound($"{word} does not exist in dictionary.");
            }
        }

        public record WordEntry(string word, string definition);

    }
}
