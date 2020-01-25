using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WordCounterService.UseCases.Commands.CountTopRepeatingWords
{
    /// <summary>
    /// Command to count the top repeating words.
    /// </summary>
    public class CountTopRepeatingWordsCommand : IRequest<IReadOnlyCollection<TopRepeatingWordsViewModel>>
    {
        /// <summary>
        /// Text file.
        /// </summary>
        public IFormFile TextFile { get; }

        /// <summary>
        /// The number of words in the top list.
        /// </summary>
        public int TopCount { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="textFile">Text file.</param>
        /// <param name="topCount">The number of words in the top list.</param>
        public CountTopRepeatingWordsCommand(IFormFile textFile, int topCount)
        {
            TextFile = textFile;
            TopCount = topCount;
        }
    }
}
