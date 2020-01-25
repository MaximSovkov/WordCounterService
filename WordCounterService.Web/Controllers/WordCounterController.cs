using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using WordCounterService.UseCases.Commands.CountTopRepeatingWords;
using WordCounterService.Web.ViewModels;

namespace WordCounterService.Web.Controllers
{
    /// <summary>
    /// The controller contains methods responsible for counting the number of repeating words in a file.
    /// </summary>
    [ApiController]
    [Route("api/word-counter")]
    public class WordCounterController : ControllerBase
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator">Mediator.</param>
        public WordCounterController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Count the number of repeating words in a file.
        /// </summary>
        /// <param name="textFile">Text file.</param>
        /// <param name="topCount">The number of words in the top list.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of words with the number of occurrences sorted in descending order.</returns>
        [HttpPost("count-top-repeating-words")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyCollection<TopRepeatingWordsViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DomainExceptionViewModel))]
        public async Task<IReadOnlyCollection<TopRepeatingWordsViewModel>> CountTopRepeatingWords([Required] IFormFile textFile,
            [Required] int topCount, CancellationToken cancellationToken)
        {
            return await mediator.Send(new CountTopRepeatingWordsCommand(textFile, topCount), cancellationToken);
        }
    }
}
