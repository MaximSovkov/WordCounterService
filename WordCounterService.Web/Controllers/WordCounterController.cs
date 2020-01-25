using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WordCounterService.Web.Controllers
{
    /// <summary>
    /// Controller contains methods responsible for counting the number of repeating words in a file.
    /// </summary>
    [ApiController]
    [Route("api/word-counter")]
    public class WordCounterController : ControllerBase
    {
        /// <summary>
        /// Count the number of repeating words in a file.
        /// </summary>
        /// <param name="textFile">Text file.</param>
        /// <param name="topCount">The number of words in the top list.</param>
        [HttpPost("top-repeating-words")]
        public async Task TopRepeatingWords(IFormFile textFile, int topCount)
        {

        }
    }
}
